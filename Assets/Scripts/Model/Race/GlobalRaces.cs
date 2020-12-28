using System;
using System.Collections.Generic;
using Database;

#nullable enable

namespace Model.Race {
    public sealed class GlobalRaces {
        private static readonly GlobalRaces Instance = new GlobalRaces();

        private IDictionary<string, IRace> _closedRacesList = new Dictionary<string, IRace>();

        public static GlobalRaces GetInstance() {
            return Instance;
        }

        public void Register(IRace race) {
            if (_closedRacesList.ContainsKey(race.SystemName)) {
                throw new InvalidOperationException("同名のRaceが登録されました");
            }

            _closedRacesList.Add(race.SystemName, race);
        }

        public IRace FindByName(string name)
        {
            if (!_closedRacesList.ContainsKey(name)) throw new KeyNotFoundException(name);
            
            return _closedRacesList[name];
        }

        public void Initialize()
        {
            _closedRacesList.Clear();

            foreach (var raceData in RaceDatabase.All())
            {
                var race = RaceFactory.Create(raceData.Value);
                Register(race);
            }
        }

        private GlobalRaces()
        {
            Initialize();
        }
    }
}
