using System.Collections.Generic;
using Database;
using Model.Race;

#nullable enable

namespace Model.Town.TownDetail {
    internal class Port : TownEntity {
        public Port(int id, string townName, TownType townType, IRace race, bool isCapital)
            : base(id, townName, townType, race, InitialDivisions(), isCapital: isCapital) { }

        private static IEnumerable<Division> InitialDivisions()
        {
            var divisions = new List<Division>();
            for (var i = 0; i < 10; i++)
            {
                divisions.Add(new Division(TerrainDatabase.Find(TerrainName.Plain.ToString())));
            }

            return divisions;
        }
    }
}
