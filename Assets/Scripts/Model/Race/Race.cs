using System.Collections.Generic;
using System.Linq;
using Database;
using Model.Goods;
using Model.Util;

#nullable enable

namespace Model.Race {
    public interface IRace : INamed {
        public IEnumerable<ConsumptionTrait> ConsumptionTraits { get; }
    }

    internal class GeneralRace: IRace {
        public string SystemName { get; }
        public string DisplayName { get; }
        public IEnumerable<ConsumptionTrait> ConsumptionTraits { get; }

        public GeneralRace(RaceData data)
        {
            RaceData baseData = data;
            
            SystemName = baseData.Name;
            DisplayName = baseData.DisplayName;
            ConsumptionTraits = baseData.Consumptions
                .Select(consumption => new ConsumptionTrait(GlobalGoods.GetInstance()
                    .FindByName(consumption.Goods.Name), consumption.amount));
        }
    }

    public static class RaceFactory {
        public static IRace Create(RaceData data) {
            return new GeneralRace(data);
        }
        
        public static IRace Create(RaceName type) {
            return new GeneralRace(RaceDatabase.Find(type));
        }
    }

    public class ConsumptionTrait {
        public GoodsEntity Goods { get; }
        public double Weight { get; }

        public ConsumptionTrait(
            GoodsEntity goods, 
            double weight
            ) {
            Goods = goods;
            Weight = weight;
        }
    }
}
