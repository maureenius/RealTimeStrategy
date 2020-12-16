using System;

#nullable enable

namespace Model.Town.Terrain {
    public sealed class Plain : ITerrain {
        private static readonly Plain Instance = new Plain();

        public static Plain GetInstance() {
            return Instance;
        }
        
        public string Name { get; }
        public Guid Id { get; }
        public string TypeName { get; }
        public TerrainType TerrainType { get; }

        private Plain() {
            Id = Guid.NewGuid();
            Name = "平地";
            TypeName = "平地";
            TerrainType = TerrainType.Plain;
        }
    }
}
