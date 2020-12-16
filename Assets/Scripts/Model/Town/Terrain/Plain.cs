namespace Model.Town.Terrain {
    public sealed class Plain : TerrainEntity {
        private static readonly Plain Instance = new Plain();

        public static Plain GetInstance() {
            return Instance;
        }

        private Plain() {
            Name = "平地";
            TerrainType = TerrainType.Plain;
        }
    }
}
