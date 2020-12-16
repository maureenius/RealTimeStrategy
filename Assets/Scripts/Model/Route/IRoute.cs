using System.Collections.Generic;
using Model.Goods;
using Model.Territory;
using Model.Town;

#nullable enable

namespace Model.Route {
    public interface IRoute {
        int Capacity { get; }
        TownEntity From { get; }
        TownEntity To { get; }
        void DoOneTurn();
        void PushCargo(Cargo cargo);
        List<Cargo> TakeCargo();
        void UpdateTensions(IEnumerable<Tension> argTensions);
        double FlowPower();
    }
    
    public class Tension
    {
        public readonly TerritoryEntity Territory;
        public bool IsForward;
        public double Power;
        
        public Tension(TerritoryEntity territory, double power)
        {
            Territory = territory;
            Power = power;
            IsForward = true;
        }
    }
}
