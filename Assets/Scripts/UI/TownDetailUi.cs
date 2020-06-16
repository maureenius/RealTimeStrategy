using System;
using System.Collections.Generic;
using System.Linq;
using ControllerInfo;
using Manager;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class TownDetailUi : MonoBehaviour
    {
        [SerializeField] private int milliSecForUpdate = 1000;
        [SerializeField] private GameObject popSlotRowPrefab;
        [SerializeField] private GameObject slotContainer;

        private TownController selectedTown;
        private Sprite[] slotBackgroundImages;
        private Sprite[] raceFaceImages;

        private void Start()
        {
            Observable.Interval(TimeSpan.FromMilliseconds(milliSecForUpdate))
                .Where(_ => selectedTown != null)
                .Subscribe(_ => UpdateTownDetail())
                .AddTo(this);
        }
        
        public void SelectTown(BaseEventData e) {
            selectedTown = ((PointerEventData)e).pointerEnter.GetComponent<TownController>();
        }

        public void Open()
        {
            if (selectedTown == null) return;
            if (raceFaceImages == null) LoadSprites();
            gameObject.SetActive(true);
            UpdateTownDetail();
        }

        private void ClearSlotContainer()
        {
            foreach (Transform child in slotContainer.transform)
            {
                Destroy(child.gameObject);
            }
        }

        private void UpdateTownDetail()
        {
            ClearSlotContainer();

            selectedTown.GetPopSlotInfo().ForEach(info =>
            {
                var (slotTypeName, popSlotInfos) = info;
                
                AddSlotRow().GetComponent<SlotRowUi>().Initialize(slotTypeName, popSlotInfos, slotBackgroundImages, raceFaceImages);
            });
        }

        private GameObject AddSlotRow()
        {
            return Instantiate(popSlotRowPrefab, slotContainer.transform, true);
        }

        private void LoadSprites()
        {
            slotBackgroundImages = Resources.LoadAll<Sprite> ("Images/SlotBackground/");
            raceFaceImages = Resources.LoadAll<Sprite> ("Images/RaceFace/");
        }
    }
}
