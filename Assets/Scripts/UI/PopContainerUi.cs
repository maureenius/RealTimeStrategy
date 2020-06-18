using System;
using System.Collections.Generic;
using System.Linq;
using ControllerInfo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PopContainerUi : MonoBehaviour
    {
        [SerializeField] private Image popFace;
        [SerializeField] private TextMeshProUGUI popName;
        [SerializeField] private TextMeshProUGUI popProduct;
        [SerializeField] private TextMeshProUGUI popConsumption;

        public void Start()
        {
            RefreshView();
        }

        public void UpdateView(PopInfo pop, Sprite face)
        {
            UpdatePopFace(face);
            UpdatePopName(pop.Name);
            UpdatePopProduct(pop.Produces);
            UpdatePopConsumption(pop.Consumptions);
        }

        public void RefreshView()
        {
            popFace.gameObject.SetActive(false);
            popName.text = "";
            popProduct.text = "";
            popConsumption.text = "";
        }

        private void UpdatePopFace(Sprite newSprite)
        {
            popFace.gameObject.SetActive(true);
            popFace.sprite = newSprite;
        }

        private void UpdatePopName(string newName)
        {
            popName.text = newName;
        }

        private void UpdatePopProduct(IEnumerable<(string, double)> products)
        {
            popProduct.text = string.Join(Environment.NewLine,
                products.Select(item => item.Item1 + " : " + item.Item2));
        }
        
        private void UpdatePopConsumption(IEnumerable<(string, double)> consumptions)
        {
            popConsumption.text = string.Join(Environment.NewLine,
                consumptions.Select(item => item.Item1 + " : " + item.Item2));
        }
    }
}