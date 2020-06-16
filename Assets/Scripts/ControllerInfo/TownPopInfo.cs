using System.Collections.Generic;
using System.Linq;

namespace ControllerInfo
{
    public readonly struct TownPopInfo
    {
        public List<PopSlotInfo> PopSlotInfos { get; }
        public List<PopInfo> PopInfos { get; }

        public TownPopInfo(IEnumerable<PopSlotInfo> popSlotInfos, IEnumerable<PopInfo> popInfos)
        {
            PopSlotInfos = popSlotInfos.ToList();
            PopInfos = popInfos.ToList();
        }
    }
}