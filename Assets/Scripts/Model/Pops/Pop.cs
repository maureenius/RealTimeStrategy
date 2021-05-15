using System;
using System.Collections.Generic;
using System.Linq;
using Model.Goods;
using Model.Race;
using Model.Util;

#nullable enable

namespace Model.Pops {
    public class Pop: INamed {
        public Guid Id { get; }
        public string SystemName { get; }
        public string DisplayName { get; }
        private string TypeName { get; }
        private IRace Race { get; }
        
        // ReSharper disable once InconsistentNaming
        private List<ProduceAbility> _produceAbilities { get; }
        public IEnumerable<ProduceAbility> ProduceAbilities => _produceAbilities;
        
        private readonly Consumer essentialConsumer = new Consumer();
        private readonly Consumer luxuryConsumer = new Consumer();
        private readonly Consumer materialConsumer = new Consumer();
        
        public Workplace? Workplace { get; private set; }

        private Professionalism professionalism = new Professionalism();
        private StandardOfLiving standardOfLiving = new StandardOfLiving();

        public Pop(IRace race)
        {
            Id = Guid.NewGuid();
            Race = race;
            _produceAbilities = new List<ProduceAbility>();
            essentialConsumer.AddTrait(race.ConsumptionTraits);

            SystemName = Race.SystemName;
            TypeName = Race.SystemName;
            DisplayName = Race.DisplayName;
        }

        public IEnumerable<ConsumptionTrait> ConsumptionTraits()
        {
            return essentialConsumer.Traits.Union(materialConsumer.Traits).Union(luxuryConsumer.Traits);
        }

        public IEnumerable<Cargo> Produce()
        {
            return ProduceAbilities.Select(pa => new Cargo(pa.OutputGoods, pa.ProduceAmount)).ToList();
        }

        public ConsumingResult ConsumeEssential(IEnumerable<Cargo> cargoes)
        {
            return essentialConsumer.Consume(cargoes);
        }

        public ConsumingResult ConsumeLuxury(IEnumerable<Cargo> cargoes)
        {
            return luxuryConsumer.Consume(cargoes);
        }

        public ConsumingResult ConsumeMaterial(IEnumerable<Cargo> cargoes)
        {
            return materialConsumer.Consume(cargoes);
        }

        public float Faith()
        {
            // TODO: 実装
            return 1f;
        }

        public void GetJob(Workplace slot)
        {
            Workplace = slot;
            materialConsumer.AddTrait(Workplace.ConsumptionTraits);
            // _produceAbilities.AddRange(Workplace.ProduceAbilities);
        }

        private string GetWorkSlotTypeName()
        {
            return Workplace == null ? "無職" : Workplace.SystemName;
        }
    }
}
