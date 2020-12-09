using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View
{
    public class PopSlotView : MonoBehaviour
    {
        [SerializeField] private GameObject background;
        [SerializeField] private GameObject pop;
        private PopSlotViewData data;
        
        private readonly Subject<PopSlotViewData> _onSelectedSubject = new Subject<PopSlotViewData>();
        public IObservable<PopSlotViewData> OnSelected => _onSelectedSubject;
        
        public void Initialize(PopSlotViewData arg)
        {
            data = arg;
            background.GetComponent<Image>().sprite = data.SlotBackgroundImage;

            if (arg.WorkerGuid == Guid.Empty) return;

            pop.GetComponent<Image>().sprite = data.WorkerImage;
        }

        public void OnClicked(BaseEventData e)
        {
            _onSelectedSubject.OnNext(data);
        }
    }

    public readonly struct PopSlotViewData
    {
        public Guid SlotGuid { get; }
        public string SlotName { get; }
        public string SlotTypeName { get; }
        public Sprite SlotBackgroundImage { get; }
        public Guid WorkerGuid { get; }
        public string WorkerName { get; }
        public string WorkerRaceName { get; }
        public Sprite WorkerImage { get; }
        
        public PopSlotViewData(Guid workerGuid, string workerName, string workerRaceName, Sprite workerImage)
        {
            SlotGuid = Guid.Empty;
            SlotName = "無職";
            SlotTypeName = "無職";
            SlotBackgroundImage = null;
            WorkerGuid = workerGuid;
            WorkerName = workerName;
            WorkerRaceName = workerRaceName;
            WorkerImage = workerImage;
        }
        
        public PopSlotViewData(Guid slotGuid, string slotName, string slotTypeName, Sprite slotBackgroundImage,
            Guid workerGuid, string workerName, string workerRaceName, Sprite workerImage)
        {
            SlotGuid = slotGuid;
            SlotName = slotName;
            SlotTypeName = slotTypeName;
            SlotBackgroundImage = slotBackgroundImage;
            WorkerGuid = workerGuid;
            WorkerName = workerName;
            WorkerRaceName = workerRaceName;
            WorkerImage = workerImage;
        }
    }
}