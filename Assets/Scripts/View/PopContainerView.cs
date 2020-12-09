using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class PopContainerView : MonoBehaviour
    {
        [SerializeField] private Image popFace;
        [SerializeField] private TextMeshProUGUI popName;
        [SerializeField] private TextMeshProUGUI popProduct;
        [SerializeField] private TextMeshProUGUI popConsumption;

        public void UpdatePop(PopSlotViewData data)
        {
            popFace.sprite = data.WorkerImage;
            popName.text = data.WorkerName;
        }

        public void RefreshView()
        {
            popFace.sprite = null;
            popName.text = "";
            popProduct.text = "";
            popConsumption.text = "";
        }
        
        private void Start()
        {
            RefreshView();
        }
    }
}