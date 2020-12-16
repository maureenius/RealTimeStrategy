#nullable enable

using System;
using Model.Util;

namespace Model.Town.Terrain {
    public enum TerrainType {
        Plain,
    }

    public interface ITerrain : INamed {
        public Guid Id { get; }
        public string TypeName { get; }
        public TerrainType TerrainType { get; }
    }
}
