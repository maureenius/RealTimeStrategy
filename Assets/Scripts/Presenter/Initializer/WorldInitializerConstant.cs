using System.Collections.Generic;
using System.Linq;
using Database;
using Model.Commerce;
using Model.Goods;
using Model.Race;
using Model.Route;
using Model.Territory;
using Model.Town;
using Model.Town.Building;
using Model.World;
using UnityEngine;
using View;

#nullable enable

namespace Presenter.Initializer
{
    public class WorldInitializerConstant : MonoBehaviour, IWorldInitializer
    {
        private void Start()
        {
            var world = InitializeWorld();
            
            GetComponent<TimePresenter>().Initialize(world);
            GetComponent<MyTerritoryPresenter>().Initialize(world.GetPlayerTerritory());
            GetComponent<TownOutlinePresenter>().Initialize();
            GetComponent<TownDetailPresenter>().Initialize();
            
            GetComponent<ViewInitializer>().Initialize();
        }

        public World InitializeWorld()
        {
            var world = new World();

            InitializeGlobals();
            InitializeTerritories(world);
            InitializeTowns(world);
            InitializeRoutes(world);
            InitializeCommerce(world);
            
            SetTerritoriesCapital(world);

            return world;
        }
        
        private static void InitializeGlobals()
        {
            GlobalRaces.GetInstance().Initialize();
            GlobalGoods.GetInstance().Initialize();
        }
        
        private void InitializeTowns(World world)
        {
            var human = GlobalRaces.GetInstance().FindByName("Human");
            var elf = GlobalRaces.GetInstance().FindByName("Elf");
            // Constant Dataをハードコードで実装
            var initialTownDatas = new List<InitialTownData>()
            {
                new InitialTownData(1, "原初の森", TownType.Inland, elf),
                new InitialTownData(2, "青碧の湖", TownType.Port, elf),
                new InitialTownData(3, "首都マクシムス", TownType.Inland, human),
                new InitialTownData(4, "ポートランド", TownType.Port, human),
                new InitialTownData(5, "クラフトランド", TownType.Inland, human)
            };
            
            // Model側を初期化
            var presenter = GetComponent<TownsPresenter>();
            // 農場5
            world.InitializeTowns(initialTownDatas
                .Select(townData =>
                {
                    var townEntity =
                        TownFactory.Create(townData.Id, townData.TownName, townData.TownType, townData.Race);

                    for (var i = 0; i < 5; i++)
                    {
                        townEntity.Build(townEntity.Divisions.ElementAt(i).Id, BuildingFactory.FlourFarm());
                    }
                    
                    presenter.InitializeEntity(townEntity);

                    return townEntity;
                }));
            
            
            // View側を初期化
            presenter.InitializeView();
        }
        
        private void InitializeTerritories(World world)
        {
            const float initialMoney = 1000f;

            world.InitializeTerritories(new List<TerritoryEntity>()
            {
                TerritoryFactory.Create("新緑教会", initialMoney),
                TerritoryFactory.CreatePlayer("魔法科学振興委員会", initialMoney)
            });
        }

        private void SetTerritoriesCapital(World world)
        {
            var elfTerritory = world.Territories.ElementAt(0);
            var elfCapital = world.Towns.First(t => t.Id == 1);
            elfTerritory.AttachTowns(elfCapital);
            world.Commerces.First(com => com.Town == elfCapital).Monopolize(elfTerritory);

            var humanTerritory = world.Territories.ElementAt(1);
            var humanCapital = world.Towns.First(t => t.Id == 3);
            humanTerritory.AttachTowns(humanCapital);
            world.Commerces.First(com => com.Town == humanCapital).Monopolize(humanTerritory);
        }

        private void InitializeRoutes(World world)
        {
            var towns = world.Towns.ToList();
            // 1-2-3-4
            //     |-5 に繋ぐ
            RouteLayer.GetInstance().Connect(towns.First(t => t.Id == 1), towns.First(t => t.Id == 2), 100, 3);
            RouteLayer.GetInstance().Connect(towns.First(t => t.Id == 2), towns.First(t => t.Id == 3), 100, 3);
            RouteLayer.GetInstance().Connect(towns.First(t => t.Id == 3), towns.First(t => t.Id == 4), 100, 3);
            RouteLayer.GetInstance().Connect(towns.First(t => t.Id == 3), towns.First(t => t.Id == 5), 100, 3);
            
            GetComponent<RoutesPresenter>().Initialize(RouteLayer.GetInstance().Routes);
        }

        private void InitializeCommerce(World world)
        {
            world.InitializeCommerce(world.Towns.Select(town => new CommerceEntity(town, world.Territories)));
        }
        
        private struct InitialTownData
        {
            public int Id;
            public string TownName;
            public TownType TownType;
            public IRace Race;

            public InitialTownData(int id, string townName, TownType townType, IRace race)
            {
                Id = id;
                TownName = townName;
                TownType = townType;
                Race = race;
            }
        }
    }
}