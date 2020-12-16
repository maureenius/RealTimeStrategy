using System;
using System.Collections.Generic;
using Model.Goods;
using Model.Util;

#nullable enable

namespace Model.Race {
    public enum RaceType {
        Human,
        Elf
    }

    public interface IRace : INamed {
        public RaceType RaceType { get; }
        public double FaithWeight { get; }

        public IList<ConsumptionTrait> ConsumptionTraits { get; }
    }

    internal class GeneralRace: IRace {
        public string Name { get; }
        public RaceType RaceType { get; }
        public double FaithWeight { get; }
        public IList<ConsumptionTrait> ConsumptionTraits { get; }
        
        public GeneralRace(string name, RaceType raceType) {
            Name = name;
            RaceType = raceType;
            var raceData = raceType switch
            {
                RaceType.Human => RaceDatabase.Human(),
                RaceType.Elf => RaceDatabase.Elf(),
                _ => throw new InvalidOperationException(raceType.ToString())
            };
            FaithWeight = raceData.FaithWeight;
            ConsumptionTraits = new List<ConsumptionTrait>(raceData.ConsumptionTraits);
        }
    }

    public static class RaceFactory {
        public static IRace Create(string name, RaceType raceType) {
            return new GeneralRace(name, raceType);
        }

        public static IRace Copy(IRace race) {
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
