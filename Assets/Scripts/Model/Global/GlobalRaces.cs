using System;
using System.Collections.Generic;
using System.Linq;
using Model.Race;

#nullable enable

namespace Model.Global {
    public sealed class GlobalRaces {
        private static readonly GlobalRaces Instance = new GlobalRaces();

        private readonly List<IRace> closedRacesList;

        public static GlobalRaces GetInstance() {
            return Instance;
        }

        public void Register(IRace race) {
            if (closedRacesList.FirstOrDefault(r => r.Name == race.Name) != null) {
                throw new InvalidOperationException("同名のRaceが登録されました");
            }

            closedRacesList.Add(RaceFactory.Copy(race));
        }

        public IRace FindByName(string name) {
            return RaceFactory.Copy(closedRacesList.Find(r => r.Name == name));
        }

        private GlobalRaces()
        {
            closedRacesList = new List<IRace>();
        }
    }
}
