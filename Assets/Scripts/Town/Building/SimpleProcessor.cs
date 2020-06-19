using System;
using System.Collections.Generic;
using Assets.Scripts.Goods;
using Assets.Scripts.Race;

namespace Assets.Scripts.Town.Building
{
    public class SimpleProcessor : IBuildable, IHasProduceAbility, IHasConsumptionTrait
    {
        public Guid Id { get; }
        public string Name { get; }
        public string TypeName { get; }
        public int SlotNum { get; }
        public IList<ProduceAbility> ProduceAbilities { get; }
        public IList<ConsumptionTrait> ConsumptionTraits { get; }
        public PopSlot SlotTemplate { get; }

        public SimpleProcessor(string name, ProduceAbility produceAbility, IEnumerable<ConsumptionTrait> consumptionTraits,
            int slotNum)
        {
            Id = Guid.NewGuid();
            Name = name;
            TypeName = name;
            ProduceAbilities = new List<ProduceAbility>{produceAbility};
            ConsumptionTraits = new List<ConsumptionTrait>(consumptionTraits);
            SlotNum = slotNum;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}