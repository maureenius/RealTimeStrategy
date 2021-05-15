using System;
using System.Collections.Generic;
using Model.Town;
using Database;
using Model.Town.Building;
using NSubstitute;
using NUnit.Framework;

#nullable enable

namespace Tests.EditMode.Editor.Model.Town
{
    public class DivisionTest
    {
        private class TerrainDataMock : ITerrainData
        {
            public string Name { get; }
            public TerrainName TerrainName { get; }
            public string DisplayName { get; }
            public int Id { get; }

            public TerrainDataMock(string name, TerrainName terrainName, string displayName, int id)
            {
                Name = name;
                TerrainName = terrainName;
                DisplayName = displayName;
                Id = id;
            }
        }
        
        // [Test]
        // public void ProvidedWorkplacesTest()
        // {
        //     var terrainData = new TerrainDataMock("test", TerrainName.Plain, "test", 1);
        //     var division = new Division(terrainData);
        //
        //     var buildingMock = Substitute.For<IBuildable>();
        //     var workplacesMock = new List<IWorkplace>()
        //     {
        //         Substitute.For<IWorkplace>()
        //     };
        //     buildingMock.Workplaces.Returns(workplacesMock);
        //     buildingMock.BuildableTerrainTypes.Returns(new List<TerrainName>() {TerrainName.Plain});
        //     
        //     division.Build(buildingMock);
        //
        //     var actual = division.ProvidedWorkplaces();
        //     var expected = workplacesMock;
        //     
        //     Assert.AreEqual(expected, actual);
        // }
    }
}