using System;
using System.Collections.Generic;
using UnityEngine;

#nullable enable

namespace Database
{
    [Serializable, CreateAssetMenu(fileName = "BuildingData", menuName = "Database/Create Building Data")]
    public class BuildingData : ScriptableObject
    {
        [SerializeField] private new string name = "";
        public string Name => name;

        [SerializeField] private BuildingName buildingName;
        public BuildingName BuildingName => buildingName;
        
        [SerializeField] private string displayName = "";
        public string DisplayName => displayName;
        
        [SerializeField] private int id;
        public int Id => id;

        [SerializeField] private List<TerrainName> buildableTerrains = new List<TerrainName>();
        public IEnumerable<TerrainName> BuildableTerrains => buildableTerrains;

        [SerializeField] private CountedWorkplace? workplace;
        public CountedWorkplace Workplace
        {
            get
            {
                if (workplace == null) throw new NullReferenceException();
                return workplace;
            }
        }
        
    }

    [Serializable]
    public class CountedWorkplace
    {
        [SerializeField] private WorkplaceData? workplace;
        public WorkplaceData Workplace
        {
            get
            {
                if (workplace == null) throw new NullReferenceException();
                return workplace;
            }
        }

        [SerializeField] public int count;
    }
}