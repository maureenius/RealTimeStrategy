using Model.Goods;

#nullable enable

namespace Model.Town {
    public class Storage {
        public GoodsEntity Goods { get; }
        public float Amount { get; private set; }
        private float AmountLimit { get; }

        public Storage(GoodsEntity goods, float amountLimit) {
            Goods = goods;
            AmountLimit = amountLimit;
        }

        public void Consume(float amount) {
            if (!CanConsume(amount)) return;

            Amount -= amount;
        }

        public void Store(float amount) {
            Amount += amount;
            if (Amount > AmountLimit) Amount = AmountLimit;
        }

        private bool CanConsume(float amount) {
            return Amount >= amount;
        }
    }
}
