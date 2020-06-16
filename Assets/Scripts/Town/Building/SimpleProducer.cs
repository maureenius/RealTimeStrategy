using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Goods;
using static Assets.Scripts.Util.Util;

namespace Assets.Scripts.Town.Building {
    public class SimpleProducer : IBuildable, IWorkable, IHasProduceAbility {
        public Guid Id { get; }
        public string Name { get; private set; }
        public string TypeName { get; }
        public IList<ProduceAbility> ProduceAbilities { get; private set; }
        public int WorkerLimit { get; private set; }

        public SimpleProducer(string name, ProduceAbility produceAbility, int workerLimit) {
            Id = Guid.NewGuid();
            Name = name;
            TypeName = name;
            ProduceAbilities = new List<ProduceAbility>();
            ProduceAbilities.Add(produceAbility);
            WorkerLimit = workerLimit;
        }

        public SimpleProducer(string _name, IList<ProduceAbility> _produceAbilities, int _workerLimit) {
            Id = Guid.NewGuid();
            Name = _name;
            TypeName = _name;
            ProduceAbilities = new List<ProduceAbility>(_produceAbilities);
            WorkerLimit = _workerLimit;
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
