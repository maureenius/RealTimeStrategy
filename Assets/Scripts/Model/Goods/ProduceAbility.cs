#nullable enable

namespace Model.Goods {
    public class ProduceAbility {
        public GoodsEntity OutputGoods { get; }
        public int ProduceAmount { get; }

        public ProduceAbility(GoodsEntity outputGoods, int produceAmount) {
            OutputGoods = outputGoods;
            ProduceAmount = produceAmount;
        }
    }
}
