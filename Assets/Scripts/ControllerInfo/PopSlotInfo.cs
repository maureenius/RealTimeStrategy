using System;

namespace ControllerInfo
{
    public readonly struct PopSlotInfo
    {
        public Guid SlotGuid { get; }
        public string SlotName { get; }
        public string SlotTypeName { get; }
        public Guid WorkerGuid { get; }
        public string WorkerName { get; }
        public string WorkerRaceName { get; }
        
        public PopSlotInfo(Guid workerGuid, string workerName, string workerRaceName)
        {
            SlotGuid = Guid.Empty;
            SlotName = "無職";
            SlotTypeName = "無職";
            WorkerGuid = workerGuid;
            WorkerName = workerName;
            WorkerRaceName = workerRaceName;
        }
        
        public PopSlotInfo(Guid slotGuid, string slotName, string slotTypeName,
            Guid workerGuid, string workerName, string workerRaceName)
        {
            SlotGuid = slotGuid;
            SlotName = slotName;
            SlotTypeName = slotTypeName;
            WorkerGuid = workerGuid;
            WorkerName = workerName;
            WorkerRaceName = workerRaceName;
        }
    }
}