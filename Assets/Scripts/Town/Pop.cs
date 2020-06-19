using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Race;
using Assets.Scripts.Goods;
using Assets.Scripts.Town.Building;
using Assets.Scripts.Util;
using ControllerInfo;
using UnityEngine;

namespace Assets.Scripts.Town {
    public class Pop: IProducable, INamed {
        public Guid Id { get; }
        public string Name { get; }
        public string TypeName { get; }
        public RaceEntity Race { get; private set; }
        public List<ProduceAbility> ProduceAbilities { get; }
        public List<ConsumptionTrait> Consumptions { get; }
        public PopSlot WorkSlot { get; private set; }

        public Pop(RaceEntity race)
        {
            Id = Guid.NewGuid();
            Race = race;
            ProduceAbilities = new List<ProduceAbility>();
            Consumptions = new List<ConsumptionTrait>(race.ConsumptionTraits);

            Name = Race.Name;
            TypeName = Race.Name;
        }

        public IList<ConsumptionTrait> RequestProducts() {
            return Race.ConsumptionTraits;
        }

        public List<Cargo> Produce()
        {
            return ProduceAbilities.Select(pa => new Cargo(pa.OutputGoods, pa.ProduceAmount)).ToList();
        }

        public void GetJob(PopSlot slot)
        {
            WorkSlot = slot;
            if (WorkSlot.ProduceAbilities != null) ProduceAbilities.AddRange(WorkSlot.ProduceAbilities);
            if (WorkSlot.ConsumptionTraits != null) Consumptions.AddRange(WorkSlot.ConsumptionTraits);
        }

        public string GetWorkSlotTypeName()
        {
            return WorkSlot == null ? "無職" : WorkSlot.TypeName;
        }

        public void Shortage()
        {
            Debug.Log("資源が足りません");
        }

        public PopSlotInfo ToSlotInfo()
        {
            if (WorkSlot == null)
            {
                return new PopSlotInfo(Id, Name, TypeName);
            }

            return new PopSlotInfo(WorkSlot.Id, WorkSlot.Name, WorkSlot.TypeName, 
                Id, Name, TypeName);
        }

        public PopInfo ToInfo()
        {
            return new PopInfo(Id, Name, TypeName,
                Consumptions.Select(trait => (trait.GoodsType.GetDescription(), trait.Weight)),
                ProduceAbilities.Select(pa => (pa.OutputGoods.Name, (double)pa.ProduceAmount)),
                WorkSlot?.Id ?? Guid.Empty, GetWorkSlotTypeName());
        }
    }
    
    public interface IProducable {
        List<ProduceAbility> ProduceAbilities { get; }
        List<Cargo> Produce();
    }
}
