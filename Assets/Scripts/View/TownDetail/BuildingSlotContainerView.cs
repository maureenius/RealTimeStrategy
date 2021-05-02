using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

#nullable enable

namespace View.TownDetail
{
    public class BuildingSlotContainerView : MonoBehaviour
    {
        [SerializeField] private Transform? buildingSlotRoot;
        [SerializeField] private GameObject? buildingSlotPrefab;
        [SerializeField] private BuildingTemplateView? buildingTemplateView;

        private readonly Subject<(Guid divisionId, Guid buildingId)> _onBuilding = 
            new Subject<(Guid divisionId, Guid buildingId)>();
        public IObservable<(Guid divisionId, Guid buildingId)> OnBuilding => _onBuilding;

        public void Initialize(IEnumerable<BuildingSlotViewData> viewDatas)
        {
            ClearSlot();
            
            foreach (var viewData in viewDatas)
            {
                AddSlot(viewData);
            }
            
            if (buildingTemplateView == null) throw new NullReferenceException();
            buildingTemplateView.OnBuildingSelected
                .Subscribe(item => _onBuilding.OnNext(item))
                .AddTo(this);
        }

        private void AddSlot(BuildingSlotViewData data)
        {
            var slot = Instantiate(buildingSlotPrefab, buildingSlotRoot)?.GetComponent<BuildingSlotView>();
            if (slot == null) throw new NullReferenceException();

            if (buildingTemplateView == null) throw new NullReferenceException();
            slot.Initialize(data);
            slot.OnSelected
                .Subscribe(slotData => buildingTemplateView.Open(slotData!.Id))
                .AddTo(this);
        }

        private void ClearSlot()
        {
            if (buildingSlotRoot == null) throw new NullReferenceException();
            
            foreach (Transform child in buildingSlotRoot.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}