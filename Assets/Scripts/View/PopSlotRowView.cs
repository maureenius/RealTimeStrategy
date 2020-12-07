using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace View
{
    public class PopSlotRowView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private GameObject popSlotPrefab;
        [SerializeField] private Transform slotContainer;

        public void Initialize(PopSlotRowViewData data)
        {
            title.text = data.Name;
            foreach (var viewData in data.PopSlotData)
            {
                AddSlot(viewData);
            }
        }
        
        private void AddSlot(PopSlotViewData data)
        {
            var slot = Instantiate(popSlotPrefab, slotContainer, true);
            slot.GetComponent<PopSlotView>().Initialize(data);
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