using System.Collections.Generic;
using Model.Goods;

#nullable enable

namespace Model.Race
{
    public static class RaceDatabase
    {
        public static RaceData Human()
        {
            return new RaceData(1.0, 
                new List<ConsumptionTrait>()
                {
                    new ConsumptionTrait(GoodsType.Flour, 1.0)
                });
        }
        
        public static RaceData Elf()
        {
            return new RaceData(2.0, 
                new List<ConsumptionTrait>()
                {
                    new ConsumptionTrait(GoodsType.Flour, 2.0)
                });
        }
    }

    public readonly struct RaceData
    {
        public readonly double FaithWeight;
        public readonly List<ConsumptionTrait> ConsumptionTraits;

        public RaceData(double faithWeight, IEnumerable<ConsumptionTrait> consumptionTraits)
        {
            FaithWeight = faithWeight;
            ConsumptionTraits = new List<ConsumptionTrait>(consumptionTraits);
        }
    }
}