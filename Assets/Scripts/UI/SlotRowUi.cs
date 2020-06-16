﻿using System;
using System.Collections.Generic;
using System.Linq;
using ControllerInfo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SlotRowUi : MonoBehaviour
    {
        [SerializeField] public TextMeshProUGUI slotName;
        [SerializeField] private GameObject slotImageRow;

        [SerializeField] private GameObject slotContainerPrefab;
        [SerializeField] private GameObject slotImagePrefab;
        [SerializeField] private GameObject popImagePrefab;

        private Sprite[] slotBackgroundImages;
        private Sprite[] raceFaceImages;
        
        public void Initialize(string title, IEnumerable<PopSlotInfo> infos, Sprite[] slotImages, Sprite[] raceImages)
        {
            slotName.text = title;
            slotBackgroundImages = slotImages;
            raceFaceImages = raceImages;
            
            infos.ToList().ForEach(AddSlot);
        }

        public void AddSlot(PopSlotInfo info)
        {
            var container = Instantiate(slotContainerPrefab, slotImageRow.transform);
            var slot = Instantiate(slotImagePrefab, container.transform);
            slot.GetComponent<Image>().sprite = FindSlotBackgroundSprite(info.SlotTypeName);

            if (info.WorkerGuid != Guid.Empty)
            {
                var pop = Instantiate(popImagePrefab, container.transform);
                pop.GetComponent<Image>().sprite = FindRaceFaceSprite(info.WorkerRaceName);
            }
        }

        public void UpdatePop(IEnumerable<PopInfo> pops)
        {
            ClearPop();
            
            pops.ToList().ForEach(pop =>
            {
                var slot = slotImageRow.GetComponentsInChildren<Transform>().First(container => container.Find("PopImagePrefab") == null);
                Instantiate(popImagePrefab, slot.transform);
            });
        }

        private void ClearPop()
        {
            slotImageRow.transform.GetComponentsInChildren<Transform>().ToList().ForEach(container =>
            {
                Destroy(container.Find("PopImagePrefab"));
            });
        }

        private Sprite FindSlotBackgroundSprite(string typeName)
        {
            var filename = "";
            switch (typeName)
            {
                case "無職":
                    filename = "Unemployed";
                    break;
                case "農場":
                    filename = "Farm";
                    break;
                default:
                    throw new InvalidOperationException(typeName);
            }
            return Array.Find(slotBackgroundImages, sprite => sprite.name.Equals(filename));
        }
        
        private Sprite FindRaceFaceSprite(string typeName)
        {
            if (typeName == null) return null;
            
            var filename = "";
            switch (typeName)
            {
                case "人間":
                    filename = "Human";
                    break;
                case "エルフ":
                    filename = "Elf";
                    break;
                default:
                    throw new InvalidOperationException(typeName);
            }
            return Array.Find(raceFaceImages, sprite => sprite.name.Equals(filename));
        }
    }
}
