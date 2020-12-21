using System;
using System.Collections.Generic;
using System.Linq;
using Model.Race;

#nullable enable

namespace Model.Global {
    public sealed class GlobalRaces {
        private static readonly GlobalRaces Instance = new GlobalRaces();

        private readonly List<IRace> _closedRacesList;

        public static GlobalRaces GetInstance() {
            return Instance;
        }

        public void Register(IRace race) {
            if (_closedRacesList.FirstOrDefault(r => r.Name == race.Name) != null) {
                throw new InvalidOperationException("同名のRaceが登録されました");
            }

            _closedRacesList.Add(RaceFactory.Copy(race));
        }

        public IRace FindByName(string name) {
            return RaceFactory.Copy(_closedRacesList.Find(r => r.Name == name));
        }

        public void Clear()
        {
            _closedRacesList.Clear();
        }

        private GlobalRaces()
        {
            _closedRacesList = new List<IRace>();
        }
    }
}
