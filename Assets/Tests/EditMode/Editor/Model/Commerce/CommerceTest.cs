// using System.Collections.Generic;
// using Model.Global;
// using Model.Race;
// using NUnit.Framework;
// using Model.Town;
// using Model.Commerce;
// using Model.Territory;
//
// namespace Tests.EditMode.Editor.Model.Commerce
// {
//     public class CommerceEntityTest
//     {
//         private CommerceEntity _commerce;
//         
//         [SetUp]
//         public void Constructor()
//         {
//             GlobalRaces.GetInstance().Register(RaceFactory.Create("testRace", RaceType.Human));
//             var race = GlobalRaces.GetInstance().FindByName("testRace");
//             var town = TownFactory.Create(0,
//                 "testTown",
//                 TownType.Port,
//                 race);
//             var territories = new List<TerritoryEntity>(){TerritoryFactory.Create("testTerritory")};
//             
//             _commerce = new CommerceEntity(town, territories);
//             
//             Assert.AreEqual(_commerce, null);
//         }
//         
//         [Test]
//         public void DoOneTurn()
//         {
//             
//         }
//     }
// }