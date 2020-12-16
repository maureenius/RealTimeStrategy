using System;
using System.Collections.Generic;
using Model.Goods;
using Model.Race;

#nullable enable

namespace Model.Town.Building
{
    public class SimpleProcessor : IBuildable, IHasProduceAbility, IHasConsumptionTrait
    {
        public Guid Id { get; }
        public string Name { get; }
        public string TypeName { get; }
        public int SlotNum { get; }
        public IList<ProduceAbility> ProduceAbilities { get; }
        public IList<ConsumptionTrait> ConsumptionTraits { get; }
        public Workplace SlotTemplate { get; }

        public SimpleProcessor(string name, ProduceAbility produceAbility, IEnumerable<ConsumptionTrait> consumptionTraits,
            int slotNum)
        {
            Id = Guid.NewGuid();
            Name = name;
            TypeName = name;
            ProduceAbilities = new List<ProduceAbility>{produceAbility};
            ConsumptionTraits = new List<ConsumptionTrait>(consumptionTraits);
            SlotNum = slotNum;
            SlotTemplate = new Workplace(this);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}