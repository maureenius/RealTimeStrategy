#nullable enable

namespace Model.Goods {
    public class Cargo {
        public GoodsEntity Goods { get; }
        public float Amount { get; }

        public Cargo(GoodsEntity goods, float amount) {
            Goods = goods;
            Amount = amount;
        }
    }
}
