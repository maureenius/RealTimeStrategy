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
        public string FacePath { get; protected set; }

        public double FaithWeight { get; protected set; }

        private readonly string resourceRootPath = "Images/RaceFace";
        protected void SetFacePath(string racePath) {
            FacePath = resourceRootPath + "/" + racePath;
        }

        public IList<ConsumptionTrait> ConsumptionTraits = new List<ConsumptionTrait>();
    }

    class GeneralRace: RaceEntity {
        public GeneralRace(string _name, RaceType _raceType) {
            Name = _name;
            RaceType = _raceType;
            
            switch (_raceType) {
                case RaceType.HUMAN:
                    SetFacePath("Human");
                    ConsumptionTraits.Add(new ConsumptionTrait(FLOUR, 1.0, _isNeccessary: true));
                    FaithWeight = 1.0;
                    break;
                case RaceType.ELF:
                    SetFacePath("Elf");
                    ConsumptionTraits.Add(new ConsumptionTrait(FLOUR, 2.0, _isNeccessary: true));
                    FaithWeight = 2.0;
                    break;
                default:
                    throw new InvalidOperationException(_raceType.ToString());
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
        public Goods.GoodsType GoodsType { get; private set; }
        public double ConsumptionWeight { get; private set; }
        readonly double outputHappinessRate;
        readonly bool isNecessary;

        public ConsumptionTrait(
            Goods.GoodsType _goodsType, 
            double _consumptionWeight, 
            double _outputHappinessRate = 1.0,
            bool _isNeccessary = false
            ) {
            GoodsType = _goodsType;
            ConsumptionWeight = _consumptionWeight;
            outputHappinessRate = _outputHappinessRate;
            isNecessary = _isNeccessary;
        }
    }
}
