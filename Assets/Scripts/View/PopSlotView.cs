using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable enable

namespace View
{
    public class PopSlotView : MonoBehaviour
    {
        [SerializeField] private GameObject? background;
        [SerializeField] private GameObject? pop;
        private PopSlotViewData _data;
        
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
            _onSelectedSubject.OnNext(_data);
        }
    }

    public readonly struct PopSlotViewData
    {
        public Guid SlotGuid { get; }
        public string? SlotName { get; }
        public Sprite? SlotBackgroundImage { get; }
        public Guid WorkerGuid { get; }
        public string? WorkerName { get; }
        public string? WorkerRaceName { get; }
        public Sprite? WorkerImage { get; }
        
        public List<(string goodsName, double amount)> Consumptions { get; }
        
        public List<(string goodsName, double amount)> Produces { get; }

        public PopSlotViewData(Guid slotGuid, string slotName, Sprite backgroundImage)
        {
            SlotGuid = slotGuid;
            SlotName = slotName;
            SlotBackgroundImage = backgroundImage;
            WorkerGuid = Guid.Empty;
            WorkerName = null;
            WorkerRaceName = null;
            WorkerImage = null;
            Consumptions = new List<(string goodsName, double amount)>();
            Produces = new List<(string goodsName, double amount)>();
        }
        
        public PopSlotViewData(Guid workerGuid, string workerName, string workerRaceName, Sprite workerImage,
            IEnumerable<(string goodsName, double amount)> consumptions, IEnumerable<(string goodsName, double amount)> produces)
        {
            SlotGuid = Guid.Empty;
            SlotName = "無職";
            SlotBackgroundImage = null;
            WorkerGuid = workerGuid;
            WorkerName = workerName;
            WorkerRaceName = workerRaceName;
            WorkerImage = workerImage;
            Consumptions = new List<(string goodsName, double amount)>(consumptions);
            Produces = new List<(string goodsName, double amount)>(produces);
        }
        
        public PopSlotViewData(Guid slotGuid, string slotName, Sprite slotBackgroundImage,
            Guid workerGuid, string workerName, string workerRaceName, Sprite workerImage,
            IEnumerable<(string goodsName, double amount)> consumptions, IEnumerable<(string goodsName, double amount)> produces)
        {
            SlotGuid = slotGuid;
            SlotName = slotName;
            SlotBackgroundImage = slotBackgroundImage;
            WorkerGuid = workerGuid;
            WorkerName = workerName;
            WorkerRaceName = workerRaceName;
            WorkerImage = workerImage;
            Consumptions = new List<(string goodsName, double amount)>(consumptions);
            Produces = new List<(string goodsName, double amount)>(produces);
        }
    }
}