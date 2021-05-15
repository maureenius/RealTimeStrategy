using System;
using System.Collections.Generic;
using System.Linq;
using Model.Town;
using TMPro;
using UnityEngine;

#nullable enable

namespace View
{
    public class TownOutlineView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI? townNameText;
        [SerializeField] private TextMeshProUGUI? townTypeText;
        [SerializeField] private List<TextMeshProUGUI> townStorageTexts = new List<TextMeshProUGUI>();
        
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
            if(townNameText == null || townTypeText == null) throw new NullReferenceException();
            
            townNameText.text = data.TownName;
            townTypeText.text = data.TownType;
            foreach (var text in townStorageTexts)
            {
                text.text = string.Empty;
            }
            
            foreach (var item in data.Storages
                .Select((storage, index) => new { storage, index } )
                .Where(item => item.index < townStorageTexts.Count))
            {
                townStorageTexts.ElementAt(item.index).text = $"{item.storage.goodsTypeName} ： {item.storage.amount}";
            }
        }
    }

    public readonly struct TownOutlineData
    {
        public string TownName { get; }
        public string TownType { get; }
        public List<(string goodsTypeName, float amount)> Storages { get; }

        public TownOutlineData(string townName, string townType, TownStorage storages)
        {
            TownName = townName;
            TownType = townType;
            Storages = storages.Storages.Select(s => (goodsTypeName: s.Goods.DisplayName, amount: s.GoodsTank.Volume)).ToList();
        }
    }
}
