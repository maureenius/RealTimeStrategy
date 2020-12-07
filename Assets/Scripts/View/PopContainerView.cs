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

        private void Start()
        {
            RefreshView();
        }

        private void RefreshView()
        {
            popFace.gameObject.SetActive(false);
            popName.text = "";
            popProduct.text = "";
            popConsumption.text = "";
        }
    }
}