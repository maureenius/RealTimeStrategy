using System;
using System.Collections.Generic;
using System.Linq;
using Model.Race;

namespace Model.Global {
    public sealed class GlobalRaces {
        private static readonly GlobalRaces Instance = new GlobalRaces();

        private readonly List<RaceEntity> closedRacesList;

        public static GlobalRaces GetInstance() {
            return Instance;
        }

        public void Register(RaceEntity race) {
            if (closedRacesList.FirstOrDefault(r => r.Name == race.Name) != null) {
                throw new InvalidOperationException("同名のRaceが登録されました");
            }

            closedRacesList.Add(RaceFactory.Copy(race));
        }

        public RaceEntity FindByName(string name) {
            return RaceFactory.Copy(closedRacesList.Find(r => r.Name == name));
        }

        private GlobalRaces()
        {
            closedRacesList = new List<RaceEntity>();
        }
    }
}
