using System;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

#nullable enable

namespace View
{
    public class PopSlotRowView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI? title;
        [SerializeField] private GameObject? popSlotPrefab;
        [SerializeField] private Transform? slotContainer;
        
        private readonly Subject<PopSlotViewData> _onPopSelectedSubject = new Subject<PopSlotViewData>();
        public IObservable<PopSlotViewData> OnPopSelected => _onPopSelectedSubject;

        public void Initialize(PopSlotRowViewData data)
        {
            if (title == null) throw new NullReferenceException();
            
            title.text = data.Name;
            foreach (var viewData in data.PopSlotData)
            {
                AddSlot(viewData);
            }
        }
        
        private void AddSlot(PopSlotViewData data)
        {
            var slot = Instantiate(popSlotPrefab, slotContainer, true)?.GetComponent<PopSlotView>();
            if (slot == null) throw new NullReferenceException();
            
            slot.Initialize(data);
            slot.OnSelected
                .Where(slotData => slotData.WorkerGuid != Guid.Empty)
                .Subscribe(slotData => _onPopSelectedSubject.OnNext(slotData))
                .AddTo(this);
        }
    }

    public readonly struct PopSlotRowViewData
    {
        public string Name { get; }
        public List<PopSlotViewData> PopSlotData { get; }

        public PopSlotRowViewData(string name, List<PopSlotViewData> data)
        {
            Name = name;
            PopSlotData = data;
        }
    }
}