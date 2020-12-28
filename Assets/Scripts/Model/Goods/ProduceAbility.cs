#nullable enable

namespace Model.Goods {
    public class ProduceAbility {
        public GoodsEntity OutputGoods { get; }
        public float ProduceAmount { get; }

        public ProduceAbility(GoodsEntity outputGoods, float produceAmount) {
            OutputGoods = outputGoods;
            ProduceAmount = produceAmount;
        }
    }
}
