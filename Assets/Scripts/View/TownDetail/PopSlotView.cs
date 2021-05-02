using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable enable

namespace View.TownDetail
{
    public class PopSlotView : MonoBehaviour
    {
        [SerializeField] private GameObject? background;
        [SerializeField] private GameObject? pop;
        private PopSlotViewData? _data;
        
        private readonly Subject<PopSlotViewData> _onSelectedSubject = new Subject<PopSlotViewData>();
        public IObservable<PopSlotViewData> OnSelected => _onSelectedSubject;
        
        public void Initialize(PopSlotViewData arg)
        {
            if(background == null || pop == null) throw new NullReferenceException();
            
            _data = arg;

            if (arg.SlotGuid == Guid.Empty)
            {
                background.GetComponent<Image>().sprite = null;
                background.GetComponent<Image>().color = new Color(255, 255, 255, 0);
            }
            else
            {
                background.GetComponent<Image>().sprite = _data.SlotBackgroundImage;
            }

            if (arg.WorkerGuid == Guid.Empty)
            {
                pop.GetComponent<Image>().sprite = null;
                pop.GetComponent<Image>().color = new Color(255, 255, 255, 0);
            }
            else
            {
                pop.GetComponent<Image>().sprite = _data.WorkerImage;
            }
        }

        public void OnClicked(BaseEventData e)
        {
            if (_data == null) throw new NullReferenceException();
            
            _onSelectedSubject.OnNext(_data);
        }
    }

    public class PopSlotViewData
    {
        public Guid SlotGuid { get; private set; }
        public string SlotName { get; private set; } = "無職";
        public Sprite? SlotBackgroundImage { get; private set; }
        public Guid WorkerGuid { get; private set; }
        public string? WorkerName { get; private set; }
        public string? WorkerRaceName { get; private set; }
        public Sprite? WorkerImage { get; private set; }

        public IEnumerable<(string goodsName, float amount)> Consumptions { get; private set; } =
            new List<(string goodsName, float amount)>();

        public IEnumerable<(string goodsName, float amount)> Produces { get; private set; } =
            new List<(string goodsName, float amount)>();

        public void SetSlot(Guid id, string name, Sprite image)
        {
            SlotGuid = id;
            SlotName = name;
            SlotBackgroundImage = image;
        }
        
        public void SetWorker(Guid id, string name, string raceName, Sprite image, 
            IEnumerable<(string goods, float amount)> produces, IEnumerable<(string goods, float amount)> consumptions)
        {
            WorkerGuid = id;
            WorkerName = name;
            WorkerRaceName = raceName;
            WorkerImage = image;
            Produces = produces;
            Consumptions = consumptions;
        }
    }
}