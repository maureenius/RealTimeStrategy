using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace View
{
    public class PopSlotRowView : MonoBehaviour
    {
        [SerializeField] private GameObject popSlotRow;
        [SerializeField] private GameObject popSlotPrefab;
        [SerializeField] private TextMeshProUGUI title;

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
            Instantiate(popSlotPrefab, popSlotRow.transform, true);
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