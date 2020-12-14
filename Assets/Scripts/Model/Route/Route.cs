using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model.Goods;
using Model.Town;
using static Model.Util.Util;

namespace Model.Route {
    public class Route : IRoute {
        public int Capacity { get; }
        public bool isForward { get; }
        public TownEntity From { get; }
        public TownEntity To { get; }

        private readonly Queue<(Cargo cargo, int timer)> cargos = new Queue<(Cargo cargo, int timer)>();
        private readonly int length;
        private List<Tension> tensions = new List<Tension>();

        public Route(int capacity, int length, TownEntity from, TownEntity to) {
            Capacity = capacity;
            isForward = true;
            this.length = length;
            From = from;
            To = to;
        }

        public void DoOneTurn() {
            cargos.ToList().ForEach(c => c.timer--);
        }

        public void PushCargo(Cargo cargo) {
            if (cargo.Amount > Capacity) return;

            cargos.Enqueue((cargo: cargo, timer: length));
        }

        public Cargo TakeCargo()
        {
            return cargos.Any(c => c.timer <= 0) ? cargos.Dequeue().cargo : null;
        }

        public double FlowPower()
        {
            return tensions.Sum(t => t.Power);
        }

        public void UpdateTensions(IEnumerable<Tension> _tensions)
        {
            tensions = tensions.Union(_tensions).ToList();
        }
    }
}
