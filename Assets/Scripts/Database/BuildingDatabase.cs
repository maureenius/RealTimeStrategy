using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable enable

namespace Database
{
    public static class BuildingDatabase
    {
        private static readonly Dictionary<string, BuildingData> BuildingDataList =
            Resources.LoadAll<BuildingData>("Database/BuildingData/")
                .ToDictionary(data => data.Name);

        public static BuildingData Find(string name)
        {
            return BuildingDataList[name];
        }

        public static BuildingData Find(BuildingName name)
        {
            return BuildingDataList[name.ToString()];
        }
    }

    public enum BuildingName
    {
        WheatField,
        SugarCaneField,
        Confectionery
    }
}