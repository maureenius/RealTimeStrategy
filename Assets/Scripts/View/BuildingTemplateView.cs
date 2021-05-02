using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

# nullable enable

namespace View
{
    public class BuildingTemplateView : MonoBehaviour
    {
        [SerializeField] private Transform? slotRoot;
        [SerializeField] private GameObject? slotPrefab;

        public IEnumerable<BuildingTemplateViewData> BuildingTemplateViewDatas { get; set; } =
            new List<BuildingTemplateViewData>();

        private readonly Subject<Guid> _onBuildingSelected = new Subject<Guid>();
        public IObservable<Guid> OnBuildingSelected => _onBuildingSelected;

        public void Open()
        {
            Refresh();
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
        
        private void Refresh()
        {
            Clear();

            foreach (var data in BuildingTemplateViewDatas)
            {
                AddSlot(data);
            }
        }

        private void Clear()
        {
            if (slotRoot == null) throw new NullReferenceException();
            
            foreach (Transform child in slotRoot)
            {
                Destroy(child);
            }
        }

        private void AddSlot(BuildingTemplateViewData data)
        {
            var slot = Instantiate(slotPrefab, slotRoot)!;
            var tileView = slot.GetComponent<BuildingTemplateTileView>();
            tileView.Initialize(data);
            tileView.OnSelected
                .Subscribe(id => _onBuildingSelected.OnNext(id))
                .AddTo(this);
        }
    }

    public class BuildingTemplateViewData
    {
        public Guid Id { get; }
        public string BuildingName { get; }
        public Sprite? BuildingImage { get; }

        public BuildingTemplateViewData(Guid id, string buildingName, Sprite buildingImage)
        {
            Id = id;
            BuildingName = buildingName;
            BuildingImage = buildingImage;
        }
    }
}