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
        
        private readonly Subject<BuildingSlotViewData> _onBuildingSelected = new Subject<BuildingSlotViewData>();
        public IObservable<BuildingSlotViewData> OnBuildingSelected => _onBuildingSelected;

        public void Initialize(IEnumerable<BuildingSlotViewData> viewDatas)
        {
            ClearSlot();
            
            foreach (var viewData in viewDatas)
            {
                AddSlot(viewData);
            }
        }

        private void AddSlot(BuildingSlotViewData data)
        {
            var slot = Instantiate(buildingSlotPrefab, buildingSlotRoot)?.GetComponent<BuildingSlotView>();
            if (slot == null) throw new NullReferenceException();
            
            slot.Initialize(data);
            slot.OnSelected
                .Where(slotData => slotData.Id != Guid.Empty)
                .Subscribe(slotData => _onBuildingSelected.OnNext(slotData))
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