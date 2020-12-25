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
        [SerializeField] private Image? image;
        
        private readonly Subject<BuildingSlotViewData> _onSelected = new Subject<BuildingSlotViewData>();
        public IObservable<BuildingSlotViewData> OnSelected => _onSelected;
        private BuildingSlotViewData _data;
        
        public void Initialize(BuildingSlotViewData data)
        {
            _data = data;
            if (image != null)
            {
                image.sprite = _data.Image;
            }
        }
    }
    
    public readonly struct BuildingSlotViewData
    {
        public Guid Id { get; }
        public string Name { get; }
        public Sprite? Image { get; }
        
        
        public BuildingSlotViewData(Guid id, string name, Sprite image)
        {
            Id = id;
            Name = name;
            Image = image;
        }
    }
}