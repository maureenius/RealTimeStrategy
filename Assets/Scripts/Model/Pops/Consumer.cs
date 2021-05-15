using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Model.Goods;

# nullable enable

namespace Model.Pops
{
    public class Consumer
    {
        private readonly List<ConsumptionTrait> traits = new List<ConsumptionTrait>();
        public IEnumerable<ConsumptionTrait> Traits => traits;

        public ConsumingResult Consume(IEnumerable<Cargo> cargoes)
        {
            var cargoList = cargoes.ToList();
            
            foreach (var trait in traits)
            {
                var targetCargo = cargoList.FirstOrDefault(c => c.IsSameGoodsType(trait.GoodsName));
                if (targetCargo == null) return ConsumingResult.Shortage;
                
                targetCargo.Amount.Consume(trait.Weight);
            }

            return ConsumingResult.Success;
        }

        public void AddTrait(ConsumptionTrait newTrait)
        {
            traits.Add(newTrait);
        }
        public void AddTrait(IEnumerable<ConsumptionTrait> newTraits)
        {
            traits.AddRange(newTraits);
        }
    }

    public enum ConsumingResult
    {
        Success = 1,
        Shortage = 2
    }
}