﻿using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace View
{
    public class TownDetailView : MonoBehaviour
    {

        [SerializeField] private GameObject popSlotRowPrefab;
        [SerializeField] private PopContainerView popContainer;
        [SerializeField] private GameObject slotContainer;
        
        private readonly Subject<Unit> _onOpenedSubject = new Subject<Unit>();
        public IObservable<Unit> OnOpened => _onOpenedSubject;
        
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

        private void UpdatePopContainer(PopSlotViewData data)
        {
            popContainer.RefreshView();
            popContainer.UpdatePop(data);
        }
        
        private PopSlotRowView AddPopSlot(PopSlotRowViewData data)
        {
            var rowView = Instantiate(popSlotRowPrefab, slotContainer.transform, true).GetComponent<PopSlotRowView>();
            rowView.Initialize(data);

            rowView.OnPopSelected
                .Subscribe(UpdatePopContainer)
                .AddTo(this);

            return rowView;
        }

        private void ClearSlots()
        {
            foreach (Transform child in slotContainer.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}