using System.Collections.Generic;
using Database;
using JetBrains.Annotations;
using Model.Town.Building;
using NSubstitute;
using NUnit.Framework;

#nullable enable

namespace Tests.EditMode.Editor.Model.Town.Building
{
    public class SimpleProducerTest
    {
        private class SimpleProducerTesting : SimpleProducer
        {
            public SimpleProducerTesting([NotNull] BuildingData data) : base(data)
            {
            }
        }

        [Test]
        public void ConstructorTest()
        {
            var workplaceDataMock = Substitute.For<WorkplaceData>();
            workplaceDataMock.Id.Returns(1);
            workplaceDataMock.Name.Returns("test");
            workplaceDataMock.DisplayName.Returns("テスト");
            workplaceDataMock.Products.Returns(new List<MeasuredGoods>());
            workplaceDataMock.Consumptions.Returns(new List<MeasuredGoods>());
            
            var countedWorkplaceMock = Substitute.For<CountedWorkplace>();
            countedWorkplaceMock.count.Returns(2);
            countedWorkplaceMock.Workplace.Returns(workplaceDataMock);
            
            var buildingDataMock = Substitute.For<BuildingData>();
            buildingDataMock.Name.Returns("test");
            buildingDataMock.DisplayName.Returns("テスト");
            buildingDataMock.BuildableTerrains.Returns(new List<TerrainName>() {TerrainName.Plain});
            buildingDataMock.Workplace.Returns(countedWorkplaceMock);

            var actual = new SimpleProducerTesting(buildingDataMock);
            Assert.AreEqual(buildingDataMock.Name, actual.SystemName);
            Assert.AreEqual(buildingDataMock.DisplayName, actual.DisplayName);
            Assert.AreEqual(buildingDataMock.BuildableTerrains, actual.BuildableTerrainTypes);
        }
    }
}