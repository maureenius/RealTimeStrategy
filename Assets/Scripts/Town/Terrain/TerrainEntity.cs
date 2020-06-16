using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Interface;

namespace Assets.Scripts.Town.Terrain {
    public enum TerrainType {
        PLAIN,
    }

    public abstract class TerrainEntity : INamed {
        public string Name { get; protected set; }
        public TerrainType TerrainType { get; protected set; }
    }
}
