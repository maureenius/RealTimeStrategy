using System;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class PopSlotView : MonoBehaviour
    {
        [SerializeField] private GameObject slotImagePrefab;
        [SerializeField] private GameObject popImagePrefab;
        
        public void Initialize(PopSlotViewData data)
        {
            var background = Instantiate(slotImagePrefab, transform);
            background.GetComponent<Image>().sprite = data.SlotBackgroundImage;

            if (data.WorkerGuid == Guid.Empty) return;

            var pop = Instantiate(popImagePrefab, transform);
            pop.GetComponent<Image>().sprite = data.WorkerImage;
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