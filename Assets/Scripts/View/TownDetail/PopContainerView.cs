using System;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable enable

namespace View.TownDetail
{
    public class PopContainerView : MonoBehaviour
    {
        [SerializeField] private Image? popFace;
        [SerializeField] private TextMeshProUGUI? popName;
        [SerializeField] private TextMeshProUGUI? popProduct;
        [SerializeField] private TextMeshProUGUI? popConsumption;

        public void UpdatePop(PopSlotViewData data)
        {
            if (popFace == null || popName == null ||
                popProduct == null || popConsumption == null) throw new NullReferenceException();
            
            popFace.sprite = data.WorkerImage;
            popName.text = data.WorkerName;
            popProduct.text = string.Join("\n", data.Produces.Select(x => $"{x.goodsName}：{x.amount.ToString(CultureInfo.CurrentCulture)}"));
            popConsumption.text = string.Join("\n", data.Consumptions.Select(x => $"{x.goodsName}：{x.amount.ToString(CultureInfo.CurrentCulture)}"));
        }

        public void RefreshView()
        {
            if (popFace == null || popName == null ||
                popProduct == null || popConsumption == null) throw new NullReferenceException();
            
            popFace.sprite = null;
            popName.text = "";
            popConsumption.text = "";
            popProduct.text = "";
        }
        
        private void Start()
        {
            RefreshView();
        }
    }
}