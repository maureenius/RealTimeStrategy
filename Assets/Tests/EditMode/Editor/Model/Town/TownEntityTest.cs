using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using JetBrains.Annotations;
using Model.Race;
using Model.Town;
using Model.Town.Building;
using NSubstitute;
using NUnit.Framework;

#nullable enable

namespace Tests.EditMode.Editor.Model.Town
{
    internal class TownEntityTesting : TownEntity
    {
        public TownEntityTesting(int id, string townName, TownType townType, IRace race, IEnumerable<IDivision> divisions, int popNum = 5, bool isCapital = false) :
            base(id, townName, townType, race, divisions, popNum, isCapital)
        {}
    }

    internal class BuildingTesting : IBuildable
    {
        public string SystemName { get; } = "test";
        public string DisplayName { get; } = "テスト";
        public Guid Id { get; } = Guid.NewGuid();
        public IEnumerable<TerrainName> BuildableTerrainTypes { get; } = new List<TerrainName>() {TerrainName.Plain};
        public IEnumerable<IWorkplace> Workplaces { get; set; } = new List<IWorkplace>()
        {
            
        };
        public IBuildable Clone()
        {
            throw new NotImplementedException();
        }
    }

    public class TownEntityTest
    {
        [Test]
        public void GetWorkplacesTest()
        {
            var raceMock = Substitute.For<IRace>();
            
            var workplaceMock1 = Substitute.For<IWorkplace>();
            var workplaceMock2 = Substitute.For<IWorkplace>();
            var workplaceMockList1 = new List<IWorkplace>() { workplaceMock1 };
            var workplaceMockList2 = new List<IWorkplace>() { workplaceMock2 };

            var terrainDataMock = Substitute.For<TerrainData>();
            terrainDataMock.Id.Returns(1);
            terrainDataMock.Name.Returns("Plain");
            terrainDataMock.TerrainName.Returns(TerrainName.Plain);
            terrainDataMock.DisplayName.Returns("平地");

            var division1 = new Division(terrainDataMock);
            var division2 = new Division(terrainDataMock);
            var divisionList = new List<IDivision>() {division1, division2};

            var building1 = new BuildingTesting();
            var building2 = new BuildingTesting();
            division1.Build(building1);
            division2.Build(building2);
            
            var town = new TownEntityTesting(1, "name", TownType.Port, raceMock, divisionList);

            var actual = town.GetWorkplaces();
            var expected = new List<IWorkplace>()
            {
                workplaceMock1,
                workplaceMock2
            };
            
            Assert.AreEqual(actual, expected);
        }
    }
}