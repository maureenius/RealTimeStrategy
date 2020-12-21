using System;
using System.Collections.Generic;
using Model.Goods;
using Model.Territory;
using Model.Town;
using UniRx;

#nullable enable

namespace Model.Route {
    public interface IRoute {
        Guid Id { get; }
        int Capacity { get; }
        TownEntity From { get; }
        TownEntity To { get; }
        void DoOneTurn();
        void PushCargo(Cargo cargo);
        IEnumerable<Cargo> TakeCargo();
        void UpdateTensions(IEnumerable<Tension> argTensions);
        double FlowPower();
        IObservable<Unit> OnRecalculation { get; }
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
