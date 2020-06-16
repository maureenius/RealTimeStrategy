using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Race;
using Assets.Scripts.Goods;
using Assets.Scripts.Town.Building;
using ControllerInfo;

namespace Assets.Scripts.Town {
    public class Pop: IProducable, INamed {
        public Guid Id { get; }
        public string Name { get; }
        public string TypeName { get; }
        public RaceEntity Race { get; private set; }
        public IList<ProduceAbility> ProduceAbilities { get; }
        public PopSlot WorkSlot { get; private set; }

        public Pop(RaceEntity race)
        {
            Id = Guid.NewGuid();
            Race = race;
            ProduceAbilities = new List<ProduceAbility>();

            Name = Race.Name;
            TypeName = Race.Name;
        }

        public IList<ConsumptionTrait> RequestProducts() {
            return Race.ConsumptionTraits;
        }

        public List<Cargo> Produce()
        {
            var produceAbility = new List<ProduceAbility>(this.ProduceAbilities);
            if (WorkSlot is IHasProduceAbility producableWorkplace)
            {
                produceAbility.AddRange(producableWorkplace.ProduceAbilities);
            }
            
            return produceAbility.Select(pa => new Cargo(pa.OutputGoods, pa.ProduceAmount)).ToList();
        }

        public void GetJob(PopSlot slot)
        {
            WorkSlot = slot;
        }

        public string GetWorkSlotTypeName()
        {
            return WorkSlot == null ? "無職" : WorkSlot.TypeName;
        }

        public PopSlotInfo ToInfo()
        {
            if (WorkSlot == null)
            {
                return new PopSlotInfo(Id, Name, TypeName);
            }

            return new PopSlotInfo(WorkSlot.Id, WorkSlot.Name, WorkSlot.TypeName, 
                Id, Name, TypeName);
        }
    }
    
    public interface IProducable {
        IList<ProduceAbility> ProduceAbilities { get; }
        List<Cargo> Produce();
    }
}
