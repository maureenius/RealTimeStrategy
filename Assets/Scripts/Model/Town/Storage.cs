using System;
using Model.Goods;
using Model.Util;

#nullable enable

namespace Model.Town {
    public class Storage {
        public GoodsEntity Goods { get; }
        public Tank GoodsTank { get; private set; }

        public Storage(GoodsEntity goods, float amountLimit) {
            Goods = goods;
            GoodsTank = new Tank(amountLimit);
        }

        public void Consume(float amount)
        {
            if (!GoodsTank.CanConsume(amount)) throw new InvalidOperationException();

            GoodsTank.Consume(amount);
        }

        public void Store(float amount) {
            GoodsTank.Store(amount);
        }

        public Cargo PickUpAll()
        {
            return PickUp(GoodsTank.Volume);
        }

        private Cargo PickUp(float amount)
        {
            Consume(amount);

            return new Cargo(Goods, amount);
        }
    }
}
