﻿using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Assets.Scripts.Global;
using Assets.Scripts.Goods;
using Assets.Scripts.Route;
using Assets.Scripts.Race;
using Assets.Scripts.Town.Building;
using Assets.Scripts.Town.Terrain;
using Assets.Scripts.Util;
using ControllerInfo;
using ModestTree;
using UnityEngine;
using static Assets.Scripts.Goods.GoodsTypeMethod;

namespace Assets.Scripts.Town {
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

        private IList<IRoute> exportRoute = new List<IRoute>();
        private IList<IRoute> importRoute = new List<IRoute>();
        
        private bool isCapital;

        public TownEntity(int _id, string _townName, TownType _townType, RaceEntity _raceEntity, int _popNum = 3, int _divisionNum = 10){
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

        public void DoOneTurn() {
            OptimizeWorkers();
            Produce();
            Consume();
            //CollectFaith();
            Import();
            Export();
        }

        public List<(string goodsTypeName, int amount)> GetStoredProducts() {
            return storages.Select(s => (goodsTypeName: s.Goods.GoodsType.GetNameJpn(), amount: s.Amount)).ToList();
        }

        public void SetRoute(IRoute route, bool isImport) {
            if (isImport) {
                importRoute.Add(route);
            } else {
                exportRoute.Add(route);
            }
        }

        public List<(string slotTypeName, IEnumerable<PopSlotInfo>)> GetPopSlotInfo()
        {
            var allInfo = pops.Select(pop => pop.ToInfo()).ToList();
            allInfo.AddRange(vacantSlots.Select(slot => slot.ToInfo()));
            var slotNames = allInfo.GroupBy(info => info.SlotTypeName).Select(grouping => grouping.Key);
            return slotNames.Select(name => (name, allInfo.Where(info => info.SlotName == name))).ToList();
        }
        
        public void Build(IBuildable template)
        {
            if (!(template.Clone() is IBuildable building)) return;
            
            buildings.Add(building);
            for (var i = 0; i < building.ProduceNum; i++)
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
                    storage.Consume((int)trait.ConsumptionWeight);
                });
            });
        }

        private void CollectFaith() {

        }

        private void Import() {
            importRoute.ToList().ForEach(ir => {
                ir.TakeCargo();
            });
        }
        private void Export() {
        }

        private void OptimizeWorkers()
        {
            GetUnemployed().ForEach(pop =>
            {
                if (vacantSlots.IsEmpty()) return;

                var targetSlot = vacantSlots.First();
                pop.GetJob(targetSlot);
                vacantSlots.Remove(targetSlot);
            });
        }

        private Storage GetStorage(Goods.GoodsEntity target) {
            return storages.FirstOrDefault(storage => storage.Goods.Name == target.Name);
        }

        private Storage GetStorageByGoodsType(Goods.GoodsType goodsType) {
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
                    throw new System.ArgumentException(_townType.ToString());
            }
        }
    }
}
