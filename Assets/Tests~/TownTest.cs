using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Global;
using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assets.Scripts.Town;
using Assets.Scripts.Global;
using Assets.Scripts.Goods;
using Assets.Scripts.Race;
using Assets.Scripts.Town.Building;
using Assert = NUnit.Framework.Assert;

namespace Tests
{
    public class TownTest
    {
        private TownEntity townEntity;
        private List<RaceEntity> races;
        private List<GoodsEntity> goods;
        
        [TestInitialize]
        public void TownInitialize()
        {
            races = new List<RaceEntity>();
            races.Add(RaceFactory.Create("Race_01", RaceType.HUMAN));
            
            goods = new List<GoodsEntity>();
            goods.Add(GoodsFactory.Create(GoodsType.FLOUR, "小麦"));
            
            var building = new SimpleProducer("農場", new ProduceAbility(goods.First(), 10), 5);
            
            townEntity = TownFactory.Create(0, "town_01", TownType.INLAND, races.First());
            townEntity.Build(0, building);
        }

        [Test]
        public void OptimizeWorkersTest()
        {
            TownInitialize();
            
            var town = new PrivateObject(townEntity, new PrivateType(typeof(TownEntity)));
            town.Invoke("OptimizeWorkers");

            var resultPops = (List<Pop>)town.GetField("pops");
            resultPops.ForEach(pop =>
            {
                Assert.IsNotNull(pop.workplace);
            });
        }
        
        [Test]
        public void ProduceTest()
        {
            TownInitialize();
            
            var town = new PrivateObject(townEntity, new PrivateType(typeof(TownEntity)));
            town.Invoke("OptimizeWorkers");
            town.Invoke("Produce");

            var results = (List<Storage>)town.GetField("storages");
            results.ForEach(result =>
            {
                Assert.AreEqual(result.Goods.Name, goods.First().Name);
                Assert.AreEqual(result.Amount, 30);
            });
        }

        [Test]
        public void ConsumeTest()
        {
            TownInitialize();
            
            var town = new PrivateObject(townEntity, new PrivateType(typeof(TownEntity)));
            var storage = new Storage(goods.First(), 100);
            storage.Store(100);
            var storages = new List<Storage>{storage};
            town.SetField("storages", storages);
            town.Invoke("Consume");

            var results = (List<Storage>) town.GetField("storages");
            results.ForEach(result =>
            {
                Assert.AreEqual(goods.First().Name, result.Goods.Name);
                Assert.AreEqual(97, result.Amount);
            });
        }
    }
}