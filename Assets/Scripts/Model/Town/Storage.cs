using Model.Goods;

namespace Model.Town {
    public class Storage {
        public GoodsEntity Goods { get; private set; }
        public int Amount { get; private set; }
        public int AmountLimit { get; private set; }

        public Storage(GoodsEntity goods, int amountLimit) {
            Goods = goods;
            AmountLimit = amountLimit;
        }

        public void Consume(int amount) {
            if (!CanConsume(amount)) return;

            Amount -= amount;
        }

        public void Store(int amount) {
            Amount += amount;
            if (Amount > AmountLimit) Amount = AmountLimit;
        }

        private bool CanConsume(int amount) {
            return Amount >= amount;
        }
    }
}
