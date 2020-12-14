using System;
using System.Collections;
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
        PORT,
        [Description("内陸")]
        INLAND,
    }

    public abstract class TownEntity {
        public readonly int Id;
        public readonly string TownName;
        public readonly TownType TownType;
        public IList<Storage> storages = new List<Storage>();
        private IList<Pop> pops = new List<Pop>();
        private IList<Workplace> workplaces = new List<Workplace>();
        
        private IList<IBuildable> buildings = new List<IBuildable>();

        // private IList<IRoute> exportRoute = new List<IRoute>();
        // private IList<IRoute> importRoute = new List<IRoute>();
        // private IList<ExportPlan> exportPlans = new List<ExportPlan>();
        
        public bool IsCapital { get; private set; }

        public TownEntity(int _id, string _townName, TownType _townType, RaceEntity _raceEntity, int _popNum = 5, int _divisionNum = 10, bool isCapital=false){
            Id = _id;
            TownName = _townName;
            TownType = _townType;
            IsCapital = isCapital;

            InitializePops(_raceEntity, _popNum);
        }

        public void InitializePops(RaceEntity race, int num) {
            pops.Clear();
            for(var i = 0; i < num; i++)
            {
                pops.Add(new Pop(race));
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
            return storages.Select(s => (goodsTypeName: s.Goods.GoodsType.GetNameJpn(), amount: s.Amount)).ToList();
        }
        
        //
        // public void RemoveRoute(IRoute route, bool isImport)
        // {
        //     if (isImport)
        //     {
        //         importRoute.Remove(route);
        //     }
        //     else
        //     {
        //         exportRoute.Remove(route);
        //     }
        // }
        //
        // public void AddExportPlan(ExportPlan plan)
        // {
        //     exportPlans.Add(plan);
        // }
        //
        // public void RemoveExportPlan(ExportPlan plan)
        // {
        //     exportPlans.Remove(plan);
        // }

        // public List<(string slotTypeName, IEnumerable<WorkplaceInfo>)> GetPopSlotInfo()
        // {
        //     var popInfos = pops.Select(pop => pop.ToInfo());
        //     var slotNames = allInfo.GroupBy(info => info.SlotTypeName).Select(grouping => grouping.Key);
        //     return slotNames.Select(name => (name, allInfo.Where(info => info.SlotName == name))).ToList();
        // }

        public List<PopData> GetPopData()
        {
            return pops.Select(pop => pop.ToData()).ToList();
        }

        public List<WorkplaceData> GetWorkplaces()
        {
            return workplaces.Select(ws => ws.ToData()).ToList();
        }
        
        public void Build(IBuildable template)
        {
            if (!(template.Clone() is IBuildable building)) return;
            
            buildings.Add(building);
            for (var i = 0; i < building.SlotNum; i++)
            {
                workplaces.Add(new Workplace(building));
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
                    storages.Add(outputStorage);
                }
                outputStorage.Store(cargo.Amount);
            }
        }

        private void Produce() {
            CarryIn(pops.Select(pop => pop.Produce()).SelectMany(product => product));
        }

        private void Consume() {
            pops.ToList().ForEach(pop =>
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

        private void CollectFaith() {

        }

        // private void Import() {
        //     importRoute.ToList().ForEach(ir => {
        //         ir.TakeCargo();
        //     });
        // }
        // private void Export() {
        //     
        // }

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
            return storages.FirstOrDefault(storage => storage.Goods.Name == target.Name);
        }

        private Storage GetStorageByGoodsType(GoodsType goodsType) {
            return storages.FirstOrDefault(storage => storage.Goods.GoodsType == goodsType);
        }

        private List<Pop> GetUnemployed()
        {
            return pops.Where(pop => pop.WorkSlot == null).ToList();
        }

        private List<Workplace> GetVacantWorkplaces()
        {
            return workplaces.Where(ws => !pops.Select(pop => pop.WorkSlot).Contains(ws)).ToList();
        }
    }

    public static class TownFactory {
        public static TownEntity Create(int _id, string _townName, TownType _townType, RaceEntity _race, bool isCapital=false) {
            switch (_townType) {
                case TownType.PORT:
                    return new Port(_id, _townName, _townType, _race, isCapital);
                case TownType.INLAND:
                    return new Inland(_id, _townName, _townType, _race, isCapital);
                default:
                    throw new ArgumentException(_townType.ToString());
            }
        }
    }
}
