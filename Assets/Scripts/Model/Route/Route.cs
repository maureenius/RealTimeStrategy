using System.Collections.Generic;
using System.Linq;
using Model.Goods;
using Model.Town;
using static Model.Util.Util;

namespace Model.Route {
    internal class Route : IRoute {
        public int Capacity { get; private set; }
        public TownEntity Sender { get; }
        public TownEntity Receiver { get; }

        private readonly Queue<(Cargo cargo, int timer)> cargos = new Queue<(Cargo cargo, int timer)>();
        private readonly int length;

        public Route(int capacity, int length, TownEntity sender, TownEntity receiver) {
            Capacity = capacity;
            this.length = length;
            Sender = sender;
            Receiver = receiver;
        }

        public void DoOneTurn() {
            cargos.ToList().ForEach(c => c.timer--);
        }

        public StatusCode PushCargo(Cargo cargo) {
            if (cargo.Amount > Capacity) return StatusCode.FAIL;

            cargos.Enqueue((cargo: cargo, timer: length));

            return StatusCode.SUCCESS;
        }

        public Cargo TakeCargo()
        {
            return cargos.Any(c => c.timer <= 0) ? cargos.Dequeue().cargo : null;
        }
    }
}
