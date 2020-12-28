using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable enable

namespace Database
{
    public static class RaceDatabase
    {
        private static readonly Dictionary<string, RaceData> RaceDataList =
            Resources.LoadAll<RaceData>("Database/RaceData/")
                .ToDictionary(data => data.Name);

        public static RaceData Find(string name)
        {
            return RaceDataList[name];
        }

        public static RaceData Find(RaceName name)
        {
            return RaceDataList[name.ToString()];
        }

        public static IReadOnlyDictionary<string, RaceData> All()
        {
            return RaceDataList;
        }
    }

    public enum RaceName
    {
        Human,
        Elf
    }
}