using System;
using System.Collections.Generic;
using System.Linq;
using Model.Goods;

namespace Model.Town.Building {
    public class SimpleProducer : IBuildable, IHasProduceAbility {
        public Guid Id { get; }
        public string Name { get; }
        public string TypeName { get; }
        public IList<ProduceAbility> ProduceAbilities { get; }
        public int SlotNum { get; }
        public PopSlot SlotTemplate { get; }

        public SimpleProducer(string name, ProduceAbility produceAbility, int workerLimit) {
            Id = Guid.NewGuid();
            Name = name;
            TypeName = name;
            ProduceAbilities = new List<ProduceAbility>();
            ProduceAbilities.Add(produceAbility);
            SlotNum = workerLimit;
        }

        public bool CanWork(Pop pop) {
            return true;
        }

        public object Clone() {
            return MemberwiseClone();
        }

        public List<Cargo> Produce() {
            return ProduceAbilities.Select(pa => new Cargo(pa.OutputGoods, pa.ProduceAmount)).ToList();
        }
    }
}
