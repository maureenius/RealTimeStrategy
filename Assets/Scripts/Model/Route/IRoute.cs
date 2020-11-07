using Model.Goods;
using Model.Town;

namespace Model.Route {
    public interface IRoute {
        int Capacity { get; }
        TownEntity Sender { get; }
        TownEntity Receiver { get; }
        void DoOneTurn();
        Util.Util.StatusCode PushCargo(Cargo cargo);
        Cargo TakeCargo();
    }
}
