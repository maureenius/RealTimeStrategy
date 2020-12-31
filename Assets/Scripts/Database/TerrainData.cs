using System;
using UnityEngine;

#nullable enable

namespace Database
{
    [Serializable, CreateAssetMenu(fileName = "TerrainData", menuName="Database/Create Terrain Data")]
    public class TerrainData : ScriptableObject, ITerrainData
    {
        [SerializeField] private new string name = "";
        public string Name => name;
        [SerializeField] private TerrainName terrainName = TerrainName.Plain;
        public TerrainName TerrainName => terrainName;

        [SerializeField] private string displayName = "";
        public string DisplayName => displayName;
        [SerializeField] private int id;
        public int Id => id;
    }

    public interface ITerrainData
    {
        public string Name { get; }
        public TerrainName TerrainName { get; }
        public string DisplayName { get; }
        public int Id { get; }
    }
}