#nullable enable

namespace Model.Goods {
    public class Cargo {
        public GoodsEntity Goods { get; }
        public int Amount { get; }

        public Cargo(GoodsEntity goods, int amount) {
            Goods = goods;
            Amount = amount;
        }
    }
}
