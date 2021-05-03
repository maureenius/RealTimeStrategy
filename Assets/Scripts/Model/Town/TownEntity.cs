using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using Model.Goods;
using Model.Race;
using Model.Town.Building;
using Model.Town.TownDetail;
using Model.Util;
using UniRx;

#nullable enable

namespace Model.Town {
    public abstract class TownEntity {
        public readonly int Id;
        public readonly string TownName;
        public readonly TownType TownType;
        public IList<Storage> Storages { get; } = new List<Storage>();
        public IList<Pop> Pops { get; } = new List<Pop>();
        public IEnumerable<IDivision> Divisions { get; private set; }

        public bool IsCapital { get; }

        protected TownEntity(int id, string townName, TownType townType, IRace race, IEnumerable<IDivision> divisions, int popNum = 5, bool isCapital=false){
            Id = id;
            TownName = townName;
            TownType = townType;
            IsCapital = isCapital;
            Divisions = new List<IDivision>(divisions);

            InitializePops(race, popNum);
        }

        private void InitializePops(IRace race, int num) {
            Pops.Clear();
            for(var i = 0; i < num; i++)
            {
                Pops.Add(new Pop(race));
            }
        }
        
        private readonly Subject<Unit> _turnPassedSubject = new Subject<Unit>();
        public IObservable<Unit> OnTurnPassed => _turnPassedSubject;

        public void DoOneTurn() {
            OptimizeWorkers();
            Produce();
            Consume();
            
            _turnPassedSubject.OnNext(Unit.Default);
        }

        public IEnumerable<(string goodsTypeName, float amount)> GetStoredProducts() {
            return Storages.Select(s => (goodsTypeName: s.Goods.DisplayName, amount: s.Amount)).ToList();
        }

        public IEnumerable<IWorkplace> GetWorkplaces()
        {
            return Divisions
                .SelectMany(division => division.ProvidedWorkplaces());
        }

        public IEnumerable<IBuildable> GetBuildings()
        {
            return Divisions
                .Select(division => division.Building)
                .WhereNotNull();
        }
        
        public Util.Util.StatusCode Build(Guid divisionId, IBuildable template)
        {
            var building = template.Clone();
            var site = Divisions.First(division => division.Id == divisionId);
            if (site.CanBuild(building))
            {
                site.Build(building);
                return Util.Util.StatusCode.Success;
            }
            else
            {
                return Util.Util.StatusCode.Fail;
            }
        }

        public void CarryIn(IEnumerable<Cargo> cargoes)
        {
            foreach (var cargo in cargoes)
            {
                var storage = GetStorage(cargo.Goods) ?? CreateStorage(cargo.Goods, 1000);

                storage.Store(cargo.Amount);
            }
        }

        private Storage CreateStorage(GoodsEntity goods, int limit)
        {
            var storage = new Storage(goods, limit);
            Storages.Add(storage);

            return storage;
        }

        private void Produce() {
            CarryIn(Pops.Select(pop => pop.Produce()).SelectMany(product => product));
        }

        private void Consume() {
            Pops.ToList().ForEach(pop =>
            {
                pop.RequestProducts().ToList().ForEach(trait =>
                {
                    var storage = GetStorage(trait.Goods);
                    if (storage == null || storage.Amount < (int)trait.Weight)
                    {
                        pop.Shortage();
                        return;
                    }
                    storage.Consume((int)trait.Weight);
                });
            });
        }

        private void OptimizeWorkers()
        {
            var vacant = GetVacantWorkplaces();
            
            GetUnemployed().ForEach(pop =>
            {
                if (vacant.Count == 0) return;

                var targetSlot = vacant.First();
                pop.GetJob(targetSlot);
                vacant.Remove(targetSlot);
            });
        }

        public float CollectFaith()
        {
            return Pops.Sum(pop => pop.Faith());
        }

        private Storage? GetStorage(GoodsEntity target) {
            return Storages.FirstOrDefault(storage => storage.Goods == target);
        }

        private List<Pop> GetUnemployed()
        {
            return Pops.Where(pop => pop.Workplace == null).ToList();
        }

        private List<IWorkplace> GetVacantWorkplaces()
        {
            return GetWorkplaces().Where(ws => !Pops.Select(pop => pop.Workplace).Contains(ws)).ToList();
        }
    }

    public static class TownFactory {
        public static TownEntity Create(int id, string townName, TownType townType, IRace race, bool isCapital=false)
        {
            return townType switch
            {
                TownType.Port => new Port(id, townName, townType, race, isCapital),
                TownType.Inland => new Inland(id, townName, townType, race, isCapital),
                _ => throw new ArgumentException(townType.ToString())
            };
        }
    }
}
