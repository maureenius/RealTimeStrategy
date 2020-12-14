using Model.Race;

namespace Model.Town.TownDetail {
    class Inland : TownEntity {
        public Inland(int _id, string _townName, TownType _townType, RaceEntity _race, bool isCapital)
            : base(_id, _townName, _townType, _race, isCapital: isCapital) { }
    }
}
