﻿using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

#nullable enable

namespace View.TownDetail
{
    public class TownDetailView : MonoBehaviour
    {

        [SerializeField] private GameObject? popSlotRowPrefab;
        [SerializeField] private PopContainerView? popContainer;
        [SerializeField] private GameObject? slotContainer;
        [SerializeField] private BuildingSlotContainerView? buildingSlotContainerView;
        
        private readonly Subject<Unit> _onOpenedSubject = new Subject<Unit>();
        public IObservable<Unit> OnOpened => _onOpenedSubject;

        private readonly Subject<(Guid divisionId, Guid buildingId)> _onBuilding = new Subject<(Guid divisionId, Guid buildingId)>();
        public IObservable<(Guid divisionId, Guid buildingId)> OnBuilding => _onBuilding;

        public void Initialize()
        {
            if (buildingSlotContainerView == null) throw new NullReferenceException();
            buildingSlotContainerView.OnBuilding
                .Subscribe(id => _onBuilding.OnNext(id))
                .AddTo(this);
        }

        public void OnContentChanged()
        {
            _onOpenedSubject.OnNext(Unit.Default);
        }
        
        public void OnOpenButtonClicked()
        {
            _onOpenedSubject.OnNext(Unit.Default);
        }
        
        public void ShowOverPanel()
        {
            gameObject.SetActive(true);
        }

        public void HideOverPanel()
        {
            gameObject.SetActive(false);
        }
        
        public void UpdateDivisionContainer(IEnumerable<PopSlotRowViewData> popSlotRowViewData)
        {
            ClearSlots();

            foreach (var rowViewData in popSlotRowViewData)
            {
                AddPopSlot(rowViewData);
            }
        }

        public void UpdateBuildingContainer(IEnumerable<BuildingSlotViewData> datas)
        {
            if (buildingSlotContainerView == null) throw new NullReferenceException();
            
            buildingSlotContainerView.Initialize(datas);
        }

        private void UpdatePopContainer(PopSlotViewData data)
        {
            if(popContainer == null) throw new NullReferenceException();
            
            popContainer.RefreshView();
            popContainer.UpdatePop(data);
        }
        
        private void AddPopSlot(PopSlotRowViewData data)
        {
            if(slotContainer == null) throw new NullReferenceException();
            
            var rowView = Instantiate(popSlotRowPrefab, slotContainer.transform, true)?.GetComponent<PopSlotRowView>();
            if(rowView == null) throw new NullReferenceException();
            
            rowView.Initialize(data);

            rowView.OnPopSelected
                .Subscribe(UpdatePopContainer)
                .AddTo(this);
        }

        private void ClearSlots()
        {
            if(slotContainer == null) throw new NullReferenceException();
            
            foreach (Transform child in slotContainer.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}