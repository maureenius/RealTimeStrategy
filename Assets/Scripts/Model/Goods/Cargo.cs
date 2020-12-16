namespace Model.Goods {
    public class Cargo {
        public GoodsEntity Goods { get; private set; }
        public int Amount { get; private set; }

        public Cargo(GoodsEntity goods, int amount) {
            Goods = goods;
            Amount = amount;
        }
    }
}
