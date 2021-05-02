using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

# nullable enable

namespace View.TownDetail
{
    public class BuildingTemplateView : MonoBehaviour
    {
        [SerializeField] private Transform? slotRoot;
        [SerializeField] private GameObject? slotPrefab;

        public IEnumerable<BuildingTemplateViewData> BuildingTemplateViewDatas { get; set; } =
            new List<BuildingTemplateViewData>();

        private Guid selectedDivisionId = Guid.Empty;

        private readonly Subject<(Guid divisionId, Guid buildingId)> _onBuildingSelected = 
            new Subject<(Guid divisionId, Guid buildingId)>();
        public IObservable<(Guid divisionId, Guid buildingId)> OnBuildingSelected => _onBuildingSelected;

        public void Open(Guid divisionId)
        {
            Refresh();
            selectedDivisionId = divisionId;
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
                Destroy(child.gameObject);
            }
        }

        private void AddSlot(BuildingTemplateViewData data)
        {
            var slot = Instantiate(slotPrefab, slotRoot)!;
            var tileView = slot.GetComponent<BuildingTemplateTileView>();
            tileView.Initialize(data);
            tileView.OnSelected
                .Subscribe(buildingId =>
                {
                    var item = (divisionId: selectedDivisionId, buildingId);
                    _onBuildingSelected.OnNext(item);
                })
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