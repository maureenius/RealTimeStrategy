using Assets.Scripts.Race;

namespace Assets.Scripts.Town {
    class Port : TownEntity {
        public Port(int _id, string _townName, TownType _townType, RaceEntity _race)
            : base(_id, _townName, _townType, _race) { }
    }
}
