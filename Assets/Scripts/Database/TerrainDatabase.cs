using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable enable

namespace Database
{
    public static class TerrainDatabase
    {
        private static readonly Dictionary<string, TerrainData> TerrainDataList = 
            Resources.LoadAll<TerrainData>("Database/TerrainData/")
                .ToDictionary(data => data.Name);

        public static TerrainData Find(string name)
        {
            return TerrainDataList[name];
        }

        public static TerrainData Find(TerrainName name)
        {
            return TerrainDataList[name.ToString()];
        }
    }

    public enum TerrainName
    {
        Plain,
        Mountain
    }
}