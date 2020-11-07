using System;
using Model.Town.Building;
using Model.Town.Terrain;

namespace Model.Town {
    public class Division {
        private readonly int id;
        private TerrainEntity terrain;
        public IBuildable Building { get; private set; }

        public Division(int _id, TerrainType _terrainType) {
            id = _id;
            switch (_terrainType) {
                case TerrainType.PLAIN:
                    terrain = Plain.GetInstance();
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        public void Build(IBuildable target) {
            if (Building != null) throw new InvalidOperationException("建物が存在する区画にbuildしました");
            Building = target;
        }

        public Util.Util.StatusCode Demolish() {
            Building = null;
            return Util.Util.StatusCode.SUCCESS;
        }
    }
}
