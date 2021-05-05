using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
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

        private readonly Queue<(Cargo cargo, int timer)> _cargos = new Queue<(Cargo cargo, int timer)>();
        private readonly int _length;
        private List<Tension> _tensions = new List<Tension>();

        public IObservable<Unit> OnRecalculation => _onRecalculation;
        private readonly Subject<Unit> _onRecalculation = new Subject<Unit>();

        public IObservable<IEnumerable<string>> OnCargoChanged => _onCargoChanged;
        private readonly Subject<IEnumerable<string>> _onCargoChanged = new Subject<IEnumerable<string>>();

        public Route(int capacity, int length, TownEntity from, TownEntity to) {
            Id = Guid.NewGuid();
            Capacity = capacity;
            IsForward = true;
            this._length = length;
            From = from;
            To = to;
        }

        public void DoOneTurn() {
            _cargos.ToList().ForEach(c => c.timer--);
            FireCargoChangedEvent();
        }

        public void PushCargo(Cargo cargo) {
            if (cargo.Amount > Capacity) return;

            _cargos.Enqueue((cargo, timer: _length));
        }

        public IEnumerable<Cargo> TakeCargo()
        {
            return _cargos.Where(c => c.timer <= 0).Select(c => c.cargo).ToList();
        }

        public double FlowPower()
        {
            return _tensions.Sum(t => t.Power);
        }

        public void UpdateTensions(IEnumerable<Tension> argTensions)
        {
            _tensions = _tensions.Union(argTensions).ToList();
            
            _onRecalculation.OnNext(Unit.Default);
        }

        private void FireCargoChangedEvent()
        {
            var cargoNames = _cargos.Select(item => item.cargo.Goods.DisplayName).Distinct();
            _onCargoChanged.OnNext(cargoNames);
        }
    }
}
