using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ControllerInfo;
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
        private IList<Pop> pops = new List<Pop>();
        private IList<PopSlot> vacantSlots = new List<PopSlot>();

        private IList<Storage> storages = new List<Storage>();
        private IList<IBuildable> buildings = new List<IBuildable>();

        // private IList<IRoute> exportRoute = new List<IRoute>();
        // private IList<IRoute> importRoute = new List<IRoute>();
        // private IList<ExportPlan> exportPlans = new List<ExportPlan>();
        
        private bool isCapital;

        public TownEntity(int _id, string _townName, TownType _townType, RaceEntity _raceEntity, int _popNum = 5, int _divisionNum = 10){
            Id = _id;
            TownName = _townName;
            TownType = _townType;

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

        // public void SetRoute(IRoute route, bool isImport) {
        //     if (isImport) {
        //         importRoute.Add(route);
        //     } else {
        //         exportRoute.Add(route);
        //     }
        // }
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

        public List<(string slotTypeName, IEnumerable<PopSlotInfo>)> GetPopSlotInfo()
        {
            var allInfo = pops.Select(pop => pop.ToSlotInfo()).ToList();
            allInfo.AddRange(vacantSlots.Select(slot => slot.ToInfo()));
            var slotNames = allInfo.GroupBy(info => info.SlotTypeName).Select(grouping => grouping.Key);
            return slotNames.Select(name => (name, allInfo.Where(info => info.SlotName == name))).ToList();
        }

        public PopInfo GetPopInfo(Guid popId)
        {
            return pops.First(pop => pop.Id.Equals(popId)).ToInfo();
        }
        
        public void Build(IBuildable template)
        {
            if (!(template.Clone() is IBuildable building)) return;
            
            buildings.Add(building);
            for (var i = 0; i < building.SlotNum; i++)
            {
                vacantSlots.Add(new PopSlot(building));
            }
        }

        private void Produce() {
            var products = pops.Select(pop => pop.Produce()).SelectMany(product => product).ToList();
            products.ForEach(cargo => {
                var outputStorage = GetStorage(cargo.Goods);
                if (outputStorage == null) {
                    outputStorage = new Storage(cargo.Goods, 1000);
                    storages.Add(outputStorage);
                }
                outputStorage.Store(cargo.Amount);
            });
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
            GetUnemployed().ForEach(pop =>
            {
                if (vacantSlots.Count == 0) return;

                var targetSlot = vacantSlots.First();
                pop.GetJob(targetSlot);
                vacantSlots.Remove(targetSlot);
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
    }

    public static class TownFactory {
        public static TownEntity Create(int _id, string _townName, TownType _townType, RaceEntity _race) {
            switch (_townType) {
                case TownType.PORT:
                    return new Port(_id, _townName, _townType, _race);
                case TownType.INLAND:
                    return new Inland(_id, _townName, _townType, _race);
                default:
                    throw new ArgumentException(_townType.ToString());
            }
        }
    }
}
