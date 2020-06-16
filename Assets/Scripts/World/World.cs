using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Goods;
using Assets.Scripts.Region;
using Assets.Scripts.Territory;
using Assets.Scripts.Town;
using Assets.Scripts.Town.Building;
using Assets.Scripts.Route;
using Assets.Scripts.Race;
using Assets.Scripts.Global;

namespace Assets.Scripts.World {
    public class World {
        public readonly List<TownEntity> Towns = new List<TownEntity>();
        public readonly List<TerritoryEntity> Territories = new List<TerritoryEntity>();
        public readonly List<RegionEntity> Regions = new List<RegionEntity>();
        public readonly RouteLayer RouteLayer = new RouteLayer();

        public DateTime Date = new DateTime(1000, 1, 1);

        public World() {
            InitializeRaces();
            InitializeGoods();
            InitializeRegion();
            InitializeTerritory();
            InitializeTown();
            InitializeRoutes();
        }

        public void DoOneTurn() {
            Date = Date.AddDays(1);
            Territories.ForEach(territory => territory.DoOneTurn());
            RouteLayer.DoOneTurn();
        }

        private void InitializeRaces() {
            // debug
            // ひとまずHumanとElfを生成
            GlobalRaces.GetInstance().Register(RaceFactory.Create("人間", RaceType.HUMAN));
            GlobalRaces.GetInstance().Register(RaceFactory.Create("エルフ", RaceType.ELF));
        }

        private void InitializeGoods() {
            // debug
            // ひとまず小麦を生成
            GlobalGoods.GetInstance().Register(GoodsFactory.Create(GoodsType.FLOUR, "普通の小麦"));
        }

        private void InitializeRegion() {
            // debug
            // ひとまず2地域を作成
            Regions.Add(RegionFactory.Create("東ジャスミニア"));
            Regions.Add(RegionFactory.Create("西ジャスミニア"));
        }

        private void InitializeTerritory() {
            // debug
            // ひとまず2勢力を作成
            Territories.Add(TerritoryFactory.Create("新緑教会"));
            Territories.Add(TerritoryFactory.Create("魔法科学振興委員会"));

            // 農場のテンプレートを保有
            var pa = new ProduceAbility(GlobalGoods.GetInstance().FindByName("普通の小麦"), 3);
            var farm = new SimpleProducer("農場", pa, 5);
            Territories.ForEach(territory => {
                territory.AddBuildingTemplate(farm);
            });
        }

        private void InitializeTown() {
            // debug
            // ひとまず5都市を作成

            // 初期Pop3, 区画10, 農場2
            TownEntity SetTown(int id, string name, TownType townType, string raceName) {
                var town = TownFactory.Create(id, name, townType, GlobalRaces.GetInstance().FindByName(raceName));

                // Regionに属させる
                // Territoryに属させる
                var regionName = "";
                var territoryName = "";
                switch (raceName)
                {
                    case "エルフ":
                        regionName = "東ジャスミニア";
                        territoryName = "新緑教会";
                        break;
                    case "人間":
                        regionName = "西ジャスミニア";
                        territoryName = "魔法科学振興委員会";
                        break;
                    default:
                        throw new InvalidOperationException("不正なRegionが指定されました");
                }
                Regions.First(r => r.Name == regionName).AttachTowns(town);
                Territories.First(t => t.Name == territoryName).AttachTowns(town);

                town.DevInitializePopSlots();
                
                return town;
            }

            Towns.Add(SetTown(1, "原初の森", TownType.INLAND, "エルフ"));
            Towns.Add(SetTown(2, "青碧の湖", TownType.PORT, "エルフ"));
            Towns.Add(SetTown(3, "首都マクシムス", TownType.INLAND, "人間"));
            Towns.Add(SetTown(4, "ポートランド", TownType.PORT, "人間"));
            Towns.Add(SetTown(5, "クラフトランド", TownType.INLAND, "人間"));
        }

        private void InitializeRoutes()
        {
            // debug
            // 1-2-3-4
            //     |-5 に繋ぐ
            RouteLayer.Connect(Towns.First(t => t.Id == 1), Towns.First(t => t.Id == 2), 100, 3);
            RouteLayer.Connect(Towns.First(t => t.Id == 2), Towns.First(t => t.Id == 3), 100, 3);
            RouteLayer.Connect(Towns.First(t => t.Id == 3), Towns.First(t => t.Id == 4), 100, 3);
            RouteLayer.Connect(Towns.First(t => t.Id == 3), Towns.First(t => t.Id == 5), 100, 3);
        }
    }
}
