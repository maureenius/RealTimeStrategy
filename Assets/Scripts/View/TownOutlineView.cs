using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace View
{
    public class TownOutlineView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI townNameText;
        [SerializeField] private TextMeshProUGUI townTypeText;
        [SerializeField] private List<TextMeshProUGUI> townStorageTexts;
        
        public void ShowOverPanel()
        {
            gameObject.SetActive(true);
        }
        
        public void HideOverPanel()
        {
            gameObject.SetActive(false);
        }

        public void UpdateOutline(TownOutlineData data)
        {
            townNameText.text = data.TownName;
            townTypeText.text = data.TownType;
            townStorageTexts.ForEach(text =>
            {
                var (goodsTypeName, amount) = data.Storages.FirstOrDefault();
                if (goodsTypeName == null) return;

                text.text = $"{goodsTypeName} ： {amount}";
                data.Storages.RemoveAt(0);
            });
        }
    }

    public readonly struct TownOutlineData
    {
        public string TownName { get; }
        public string TownType { get; }
        public List<(string goodsTypeName, int amount)> Storages { get; }

        public TownOutlineData(string townName, string townType,
            IEnumerable<(string goodsTypeName, int amount)> storages)
        {
            TownName = townName;
            TownType = townType;
            Storages = new List<(string goodsTypeName, int amount)>(storages);
        }
    }
}
