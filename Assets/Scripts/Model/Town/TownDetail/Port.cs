using Model.Race;

namespace Model.Town.TownDetail {
    internal class Port : TownEntity {
        public Port(int id, string townName, TownType townType, RaceEntity race, bool isCapital)
            : base(id, townName, townType, race, isCapital: isCapital) { }
    }
}
