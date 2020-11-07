using Model.Goods;

namespace Model.Town {
    public class Storage {
        public GoodsEntity Goods { get; private set; }
        public int Amount { get; private set; }
        public int AmountLimit { get; private set; }

        public Storage(GoodsEntity _goods, int _amountLimit) {
            Goods = _goods;
            AmountLimit = _amountLimit;
        }

        public Util.Util.StatusCode Consume(int amount) {
            if (!CanConsume(amount)) return Util.Util.StatusCode.FAIL;

            Amount -= amount;
            return Util.Util.StatusCode.SUCCESS;
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
