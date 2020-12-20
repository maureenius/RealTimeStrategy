using System;
using System.Collections.Generic;
using System.Linq;
using Model.Goods;
using Model.Town;
using UniRx;

#nullable enable

namespace Model.Route {
    public class Route : IRoute {
        public Guid Id { get; }
        public int Capacity { get; }
        public bool IsForward { get; }

        public TownEntity From { get; }

        public TownEntity To { get; }

        private readonly Queue<(Cargo cargo, int timer)> cargos = new Queue<(Cargo cargo, int timer)>();
        private readonly int length;
        private List<Tension> tensions = new List<Tension>();

        public IObservable<Unit> OnRecalculation => _onRecalculation;
        private readonly Subject<Unit> _onRecalculation = new Subject<Unit>();

        public Route(int capacity, int length, TownEntity from, TownEntity to) {
            Id = Guid.NewGuid();
            Capacity = capacity;
            IsForward = true;
            this.length = length;
            From = from;
            To = to;
        }

        public void DoOneTurn() {
            cargos.ToList().ForEach(c => c.timer--);
        }

        public void PushCargo(Cargo cargo) {
            if (cargo.Amount > Capacity) return;

            cargos.Enqueue((cargo, timer: length));
        }

        public List<Cargo> TakeCargo()
        {
            return cargos.Where(c => c.timer <= 0).Select(c => c.cargo).ToList();
        }

        public double FlowPower()
        {
            return tensions.Sum(t => t.Power);
        }

        public void UpdateTensions(IEnumerable<Tension> argTensions)
        {
            tensions = tensions.Union(argTensions).ToList();
            
            _onRecalculation.OnNext(Unit.Default);
        }
    }
}
