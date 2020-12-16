using System;
using System.Collections.Generic;
using Model.Goods;
using Model.Race;
using Model.Town.Building;
using Model.Util;

#nullable enable

namespace Model.Town
{
    public readonly struct WorkplaceData
    {
        public Guid SlotGuid { get; }
        public string SlotName { get; }
        public string SlotTypeName { get; }

        public WorkplaceData(Guid slotGuid, string slotName, string slotTypeName)
        {
            SlotGuid = slotGuid;
            SlotName = slotName;
            SlotTypeName = slotTypeName;
        }
    }
    
    public class Workplace : INamed, IHasProduceAbility
    {
        public Guid Id { get; }
        public string Name { get; }
        public string TypeName { get; }
        public IList<ProduceAbility> ProduceAbilities { get; }
        public IList<ConsumptionTrait> ConsumptionTraits { get; }
        private IBuildable Source { get; }

        public Workplace(IBuildable source)
        {
            Source = source;
            Id = Guid.NewGuid();
            Name = Source.Name;
            TypeName = Source.TypeName;
            ProduceAbilities = Source is IHasProduceAbility pa ? new List<ProduceAbility>(pa.ProduceAbilities) : new List<ProduceAbility>();
            ConsumptionTraits = Source is IHasConsumptionTrait ct ? new List<ConsumptionTrait>(ct.ConsumptionTraits) : new List<ConsumptionTrait>();
        }

        public WorkplaceData ToData()
        {
            return new WorkplaceData(Id, Name, TypeName);
        }
    }
}