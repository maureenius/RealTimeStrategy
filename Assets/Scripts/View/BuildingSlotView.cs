#nullable enable
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

#nullable enable

namespace View
{
    public class BuildingSlotView : MonoBehaviour
    {
        [SerializeField] private Image? buildingImage;
        [SerializeField] private Image? terrainImage;
        
        private readonly Subject<BuildingSlotViewData> _onSelected = new Subject<BuildingSlotViewData>();
        public IObservable<BuildingSlotViewData> OnSelected => _onSelected;
        private BuildingSlotViewData _data;
        
        public void Initialize(BuildingSlotViewData data)
        {
            _data = data;
            if (terrainImage != null)
            {
                terrainImage.sprite = _data.TerrainImage;
            }

            if (buildingImage != null)
            {
                buildingImage.sprite = _data.BuildingImage;
            }
        }
    }
    
    public class BuildingSlotViewData
    {
        public Guid Id { get; }
        public string TerrainName { get; }
        public Sprite TerrainImage { get; }
        public string BuildingName { get; } = "";
        public Sprite? BuildingImage { get; }
        
        
        public BuildingSlotViewData(Guid id, string terrainName, Sprite terrainImage)
        {
            Id = id;
            TerrainName = terrainName;
            TerrainImage = terrainImage;
        }

        public BuildingSlotViewData(Guid id, string terrainName, Sprite terrainImage,
            string buildingName, Sprite buildingImage)
        {
            Id = id;
            TerrainName = terrainName;
            TerrainImage = terrainImage;
            BuildingName = buildingName;
            BuildingImage = buildingImage;
        }
    }
}