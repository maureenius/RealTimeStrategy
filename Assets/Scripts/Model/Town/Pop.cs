using System;
using System.Collections.Generic;
using System.Linq;
using Model.Goods;
using Model.Race;
using Model.Util;
using UnityEngine;

#nullable enable

namespace Model.Town {
    public class Pop: IProducable, INamed {
        public Guid Id { get; }
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
        public IWorkplace? Workplace { get; private set; }

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

        public float Faith()
        {
            // TODO: 実装
            return 1f;
        }

        public void GetJob(IWorkplace slot)
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
    }
    
    public interface IProducable {
        IEnumerable<ProduceAbility> ProduceAbilities { get; }
        IEnumerable<Cargo> Produce();
    }
}
