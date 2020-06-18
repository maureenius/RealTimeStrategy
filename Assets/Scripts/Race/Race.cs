using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assets.Scripts.Goods.GoodsType;
using UnityEngine;

namespace Assets.Scripts.Race {
    public enum RaceType {
        HUMAN,
        ELF
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
                case RaceType.HUMAN:
                    ConsumptionTraits.Add(new ConsumptionTrait(FLOUR, 1.0));
                    FaithWeight = 1.0;
                    break;
                case RaceType.ELF:
                    ConsumptionTraits.Add(new ConsumptionTrait(FLOUR, 2.0));
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
        public Goods.GoodsType GoodsType { get; }
        public double Weight { get; }

        public ConsumptionTrait(
            Goods.GoodsType goodsType, 
            double weight
            ) {
            GoodsType = goodsType;
            Weight = weight;
        }
    }
}
