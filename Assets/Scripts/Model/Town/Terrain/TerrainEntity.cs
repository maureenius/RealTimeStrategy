using System;
using Model.Util;

namespace Model.Town.Terrain {
    public enum TerrainType {
        PLAIN,
    }

    public abstract class TerrainEntity : INamed {
        public Guid Id { get; }
        public string Name { get; protected set; }

        public string TypeName { get; }
        public TerrainType TerrainType { get; protected set; }
    }
}
