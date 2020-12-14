using System;
using System.Collections.Generic;
using Model.Goods;
using Model.Territory;
using Model.Town;

namespace Model.Route {
    public interface IRoute {
        int Capacity { get; }
        TownEntity From { get; }
        TownEntity To { get; }
        void DoOneTurn();
        void PushCargo(Cargo cargo);
        Cargo TakeCargo();
        void UpdateTensions(IEnumerable<Tension> tensions);
        double FlowPower();
    }
    
    public struct Tension
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
