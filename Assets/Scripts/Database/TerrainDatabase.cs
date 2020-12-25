using System;
using System.Collections.Generic;
using UnityEngine;

namespace Database
{
    public class TerrainDatabase : ScriptableObject
    {
        [SerializeField]
        public List<TerrainData> TerrainDatas = new List<TerrainData>();
    }

    [Serializable]
    public class TerrainData : ScriptableObject
    {
        public string Name;
    }
}