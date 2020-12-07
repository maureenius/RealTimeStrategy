using System;
using System.Collections.Generic;
using System.Linq;
using Model.Goods;
using Model.Race;
using Model.Util;
using UnityEngine;

namespace Model.Town {
    public readonly struct PopData
    {
        public Guid Id { get; }
        public string Name { get; }
        public string TypeName { get; }
        public List<(string goodsName, double amount)> Consumptions { get; }
        public List<(string goodsName, double amount)> Produces { get; }
        public Guid WorkplaceGuid { get; }
        public string WorkplaceName { get; }

        public PopData(Guid id, string name, string typeName,
            IEnumerable<(string goodsName, double amount)> consumptions, 
            IEnumerable<(string goodsName, double amount)> produces,
            Guid workplaceGuid, string workplaceName)
        {
            Id = id;
            Name = name;
            TypeName = typeName;
            Consumptions = consumptions.ToList();
            Produces = produces.ToList();
            WorkplaceGuid = workplaceGuid;
            WorkplaceName = workplaceName;
        }
    }
    
    public class Pop: IProducable, Util.INamed {
        public Guid Id { get; }
        public string Name { get; }
        public string TypeName { get; }
        public RaceEntity Race { get; private set; }
        public List<ProduceAbility> ProduceAbilities { get; }
        public List<ConsumptionTrait> Consumptions { get; }
        public Workplace WorkSlot { get; private set; }

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

        public void GetJob(Workplace slot)
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

        public PopData ToData()
        {
            return new PopData(Id, Name, TypeName,
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
