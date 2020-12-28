using System;
using System.Collections.Generic;
using System.Linq;
using Model.Goods;
using Model.Race;
using Model.Util;
using UnityEngine;

#nullable enable

namespace Model.Town {
    public readonly struct PopData
    {
        public Guid Id { get; }
        public string Name { get; }
        public string TypeName { get; }
        public List<(string goodsName, double amount)> Consumptions { get; }
        public List<(string goodsName, double amount)> Produces { get; }
        public Guid WorkplaceGuid { get; }
        private string WorkplaceName { get; }

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
    
    public class Pop: IProducable, INamed {
        private Guid Id { get; }
        public string SystemName { get; }
        public string DisplayName { get; }
        private string TypeName { get; }
        private IRace Race { get; }
        
        // ReSharper disable once InconsistentNaming
        private List<ProduceAbility> _produceAbilities { get; }
        public IEnumerable<ProduceAbility> ProduceAbilities => _produceAbilities;
        
        // ReSharper disable once InconsistentNaming
        private List<ConsumptionTrait> _consumptionTraits { get; }
        public IEnumerable<ConsumptionTrait> Consumptions => _consumptionTraits;
        public Workplace? Workplace { get; private set; }

        public Pop(IRace race)
        {
            Id = Guid.NewGuid();
            Race = race;
            _produceAbilities = new List<ProduceAbility>();
            _consumptionTraits = new List<ConsumptionTrait>(race.ConsumptionTraits);

            SystemName = Race.SystemName;
            TypeName = Race.SystemName;
            DisplayName = Race.DisplayName;
        }

        public IEnumerable<ConsumptionTrait> RequestProducts() {
            return Race.ConsumptionTraits;
        }

        public IEnumerable<Cargo> Produce()
        {
            return ProduceAbilities.Select(pa => new Cargo(pa.OutputGoods, pa.ProduceAmount)).ToList();
        }

        public void GetJob(Workplace slot)
        {
            Workplace = slot;
            _produceAbilities.AddRange(Workplace.ProduceAbilities);
            _consumptionTraits.AddRange(Workplace.ConsumptionTraits);
        }

        private string GetWorkSlotTypeName()
        {
            return Workplace == null ? "無職" : Workplace.SystemName;
        }

        public void Shortage()
        {
            Debug.Log("資源が足りません");
        }

        public PopData ToData()
        {
            return new PopData(Id, SystemName, TypeName,
                Consumptions.Select(trait => (trait.Goods.DisplayName, trait.Weight)),
                ProduceAbilities.Select(pa => (pa.OutputGoods.DisplayName, (double)pa.ProduceAmount)),
                Workplace?.Id ?? Guid.Empty, GetWorkSlotTypeName());
        }
    }
    
    public interface IProducable {
        IEnumerable<ProduceAbility> ProduceAbilities { get; }
        IEnumerable<Cargo> Produce();
    }
}
