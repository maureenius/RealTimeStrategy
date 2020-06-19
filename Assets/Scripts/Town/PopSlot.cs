using System;
using System.Collections.Generic;
using Assets.Scripts.Goods;
using Assets.Scripts.Race;
using Assets.Scripts.Town.Building;
using Assets.Scripts.Town.Terrain;
using ControllerInfo;

namespace Assets.Scripts.Town
{
    public class PopSlot : INamed, IHasProduceAbility
    {
        public Guid Id { get; }
        public string Name { get; }
        public string TypeName { get; }
        public IList<ProduceAbility> ProduceAbilities { get; }
        public IList<ConsumptionTrait> ConsumptionTraits { get; }
        private IBuildable Source { get; }

        public PopSlot(IBuildable source)
        {
            Source = source;
            Id = Guid.NewGuid();
            Name = Source.Name;
            TypeName = Source.TypeName;
            if (Source is IHasProduceAbility pa) ProduceAbilities = pa.ProduceAbilities;
            if (Source is IHasConsumptionTrait ct) ConsumptionTraits = ct.ConsumptionTraits;
        }

        public PopSlotInfo ToInfo()
        {
            return new PopSlotInfo(Id, Name, TypeName, 
                Guid.Empty, null, null);
        }
    }
}