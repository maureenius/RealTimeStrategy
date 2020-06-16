using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Town.Terrain {
    public sealed class Plain : TerrainEntity {
        private static readonly Plain _plain = new Plain();

        public static Plain GetInstance() {
            return _plain;
        }

        private Plain() {
            Name = "平地";
            TerrainType = TerrainType.PLAIN;
        }
    }
}
