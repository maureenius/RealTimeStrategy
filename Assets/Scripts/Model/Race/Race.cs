using System;
using System.Collections.Generic;
using Model.Goods;

namespace Model.Race {
    public enum RaceType {
        Human,
        Elf
    }

    public abstract class RaceEntity {
        public RaceType RaceType { get; protected set; }
        public string Name { get; protected set; }

        public double FaithWeight { get; protected set; }

        public IList<ConsumptionTrait> ConsumptionTraits = new List<ConsumptionTrait>();
    }

    class GeneralRace: RaceEntity {
        public GeneralRace(string name, RaceType raceType) {
            Name = name;
            RaceType = raceType;
            
            switch (raceType) {
                case RaceType.Human:
                    ConsumptionTraits.Add(new ConsumptionTrait(GoodsType.Flour, 1.0));
                    FaithWeight = 1.0;
                    break;
                case RaceType.Elf:
                    ConsumptionTraits.Add(new ConsumptionTrait(GoodsType.Flour, 2.0));
                    FaithWeight = 2.0;
                    break;
                default:
                    throw new InvalidOperationException(raceType.ToString());
            }
        }
    }

    public static class RaceFactory {
        public static RaceEntity Create(string name, RaceType raceType) {
            return new GeneralRace(name, raceType);
        }

        public static RaceEntity Copy(RaceEntity race) {
            return new GeneralRace(race.Name, race.RaceType);
        }
    }

    public class ConsumptionTrait {
        public GoodsType GoodsType { get; }
        public double Weight { get; }

        public ConsumptionTrait(
            GoodsType goodsType, 
            double weight
            ) {
            GoodsType = goodsType;
            Weight = weight;
        }
    }
}
