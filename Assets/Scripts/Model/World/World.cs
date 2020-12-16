using System;
using System.Collections.Generic;
using System.Linq;
using Model.Commerce;
using Model.Global;
using Model.Goods;
using Model.Race;
using Model.Region;
using Model.Route;
using Model.Territory;
using Model.Town;
using Model.Town.Building;

namespace Model.World {
    public class World {
        public readonly List<TownEntity> Towns = new List<TownEntity>();
        public readonly List<CommerceEntity> Commerces = new List<CommerceEntity>();
        public readonly List<TerritoryEntity> Territories = new List<TerritoryEntity>();
        public readonly List<RegionEntity> Regions = new List<RegionEntity>();

        public DateTime Date = new DateTime(1000, 1, 1);

        public World() {
            InitializeRaces();
            InitializeGoods();
            InitializeRegion();
            InitializeTerritory();
            InitializeTown();
            InitializeRoutes();
            InitializeCommerces();
        }

        public void DoOneTurn() {
            Date = Date.AddDays(1);
            Territories.ForEach(territory => territory.DoOneTurn());
            Commerces.ForEach(commerce => commerce.DoOneTurn());
            RouteLayer.GetInstance().DoOneTurn();
        }

        private void InitializeRaces() {
            // debug
            // ひとまずHumanとElfを生成
            GlobalRaces.GetInstance().Register(RaceFactory.Create("人間", RaceType.Human));
            GlobalRaces.GetInstance().Register(RaceFactory.Create("エルフ", RaceType.Elf));
        }

        private void InitializeGoods() {
            // debug
            // ひとまず小麦を生成
            GlobalGoods.GetInstance().Register(GoodsFactory.Create(GoodsType.Flour, "普通の小麦"));
            GlobalGoods.GetInstance().Register(GoodsFactory.Create(GoodsType.Sugar, "普通の砂糖"));
            GlobalGoods.GetInstance().Register(GoodsFactory.Create(GoodsType.Cookie, "普通のクッキー"));
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
            TownEntity SetTown(int id, string name, TownType townType, string raceName, bool isCapital=false) {
                var town = TownFactory.Create(id, name, townType, GlobalRaces.GetInstance().FindByName(raceName), isCapital);

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

                return town;
            }

            Towns.Add(SetTown(1, "原初の森", TownType.Inland, "エルフ", isCapital: true));
            Towns.Add(SetTown(2, "青碧の湖", TownType.Port, "エルフ"));
            Towns.Add(SetTown(3, "首都マクシムス", TownType.Inland, "人間", isCapital: true));
            Towns.Add(SetTown(4, "ポートランド", TownType.Port, "人間"));
            Towns.Add(SetTown(5, "クラフトランド", TownType.Inland, "人間"));
            
            // Territory毎の初期化
            Territories.ForEach(territory => territory.InitializeTowns());
        }

        private void InitializeCommerces()
        {
            foreach (var town in Towns)
            {
                Commerces.Add(new CommerceEntity(town, Territories));
            }
        }

        private void InitializeRoutes()
        {
            // debug
            // 1-2-3-4
            //     |-5 に繋ぐ
            RouteLayer.GetInstance().Connect(Towns.First(t => t.Id == 1), Towns.First(t => t.Id == 2), 100, 3);
            RouteLayer.GetInstance().Connect(Towns.First(t => t.Id == 2), Towns.First(t => t.Id == 3), 100, 3);
            RouteLayer.GetInstance().Connect(Towns.First(t => t.Id == 3), Towns.First(t => t.Id == 4), 100, 3);
            RouteLayer.GetInstance().Connect(Towns.First(t => t.Id == 3), Towns.First(t => t.Id == 5), 100, 3);
        }
    }
}
