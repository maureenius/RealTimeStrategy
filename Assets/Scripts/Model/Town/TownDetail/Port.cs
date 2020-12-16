using Model.Race;

#nullable enable

namespace Model.Town.TownDetail {
    internal class Port : TownEntity {
        public Port(int id, string townName, TownType townType, IRace race, bool isCapital)
            : base(id, townName, townType, race, isCapital: isCapital) { }
    }
}
