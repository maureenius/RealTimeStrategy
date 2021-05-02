using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

#nullable enable

namespace View
{
    public class BuildingSlotContainerView : MonoBehaviour
    {
        [SerializeField] private Transform? buildingSlotRoot;
        [SerializeField] private GameObject? buildingSlotPrefab;
        [SerializeField] private BuildingTemplateView? buildingTemplateView;

        private readonly Subject<Guid> _onBuilding = new Subject<Guid>();
        public IObservable<Guid> OnBuilding => _onBuilding;

        public void Initialize(IEnumerable<BuildingSlotViewData> viewDatas)
        {
            ClearSlot();
            
            foreach (var viewData in viewDatas)
            {
                AddSlot(viewData);
            }
            
            if (buildingTemplateView == null) throw new NullReferenceException();
            buildingTemplateView.OnBuildingSelected
                .Subscribe(id => _onBuilding.OnNext(id))
                .AddTo(this);
        }

        private void AddSlot(BuildingSlotViewData data)
        {
            var slot = Instantiate(buildingSlotPrefab, buildingSlotRoot)?.GetComponent<BuildingSlotView>();
            if (slot == null) throw new NullReferenceException();

            if (buildingTemplateView == null) throw new NullReferenceException();
            slot.Initialize(data);
            slot.OnSelected
                .Subscribe(slotData => buildingTemplateView.Open())
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