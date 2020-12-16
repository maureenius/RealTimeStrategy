using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Model.Goods;
using Model.Race;
using Model.Town.Building;
using Model.Town.TownDetail;
using UniRx;

namespace Model.Town {
    public enum TownType {
        [Description("港町")]
        Port,
        [Description("内陸")]
        Inland,
    }

    public abstract class TownEntity {
        public readonly int Id;
        public readonly string TownName;
        public readonly TownType TownType;
        public IList<Storage> Storages { get; } = new List<Storage>();
        public IList<Pop> Pops { get; } = new List<Pop>();
        public IList<Workplace> Workplaces { get; } = new List<Workplace>();

        public IList<IBuildable> Buildings { get; } = new List<IBuildable>();

        public bool IsCapital { get; private set; }

        public TownEntity(int id, string townName, TownType townType, RaceEntity raceEntity, int popNum = 5, int divisionNum = 10, bool isCapital=false){
            Id = id;
            TownName = townName;
            TownType = townType;
            IsCapital = isCapital;

            InitializePops(raceEntity, popNum);
        }

        public void InitializePops(RaceEntity race, int num) {
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
            // Import();
            // Export();
            
            _turnPassedSubject.OnNext(Unit.Default);
        }

        public IEnumerable<(string goodsTypeName, int amount)> GetStoredProducts() {
            return Storages.Select(s => (goodsTypeName: s.Goods.GoodsType.GetNameJpn(), amount: s.Amount)).ToList();
        }

        public List<PopData> GetPopData()
        {
            return Pops.Select(pop => pop.ToData()).ToList();
        }

        public List<WorkplaceData> GetWorkplaces()
        {
            return Workplaces.Select(ws => ws.ToData()).ToList();
        }
        
        public void Build(IBuildable template)
        {
            if (!(template.Clone() is IBuildable building)) return;
            
            Buildings.Add(building);
            for (var i = 0; i < building.SlotNum; i++)
            {
                Workplaces.Add(new Workplace(building));
            }
        }

        public void CarryIn(IEnumerable<Cargo> cargoes)
        {
            foreach (var cargo in cargoes)
            {
                if (cargo == null) return;
                
                var outputStorage = GetStorage(cargo.Goods);
                if (outputStorage == null) {
                    outputStorage = new Storage(cargo.Goods, 1000);
                    Storages.Add(outputStorage);
                }
                outputStorage.Store(cargo.Amount);
            }
        }

        private void Produce() {
            CarryIn(Pops.Select(pop => pop.Produce()).SelectMany(product => product));
        }

        private void Consume() {
            Pops.ToList().ForEach(pop =>
            {
                pop.RequestProducts().ToList().ForEach(trait =>
                {
                    var storage = GetStorageByGoodsType(trait.GoodsType);
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

        private Storage GetStorage(GoodsEntity target) {
            return Storages.FirstOrDefault(storage => storage.Goods.Name == target.Name);
        }

        private Storage GetStorageByGoodsType(GoodsType goodsType) {
            return Storages.FirstOrDefault(storage => storage.Goods.GoodsType == goodsType);
        }

        private List<Pop> GetUnemployed()
        {
            return Pops.Where(pop => pop.WorkSlot == null).ToList();
        }

        private List<Workplace> GetVacantWorkplaces()
        {
            return Workplaces.Where(ws => !Pops.Select(pop => pop.WorkSlot).Contains(ws)).ToList();
        }
    }

    public static class TownFactory {
        public static TownEntity Create(int id, string townName, TownType townType, RaceEntity race, bool isCapital=false)
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
