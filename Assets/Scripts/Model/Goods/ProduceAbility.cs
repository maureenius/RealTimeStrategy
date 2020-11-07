namespace Model.Goods {
    public class ProduceAbility {
        public GoodsEntity InputGoods { get; set; }
        public int InputAmount { get; private set; }
        public GoodsEntity OutputGoods { get; set; }
        public int ProduceAmount { get; set; }

        public ProduceAbility(GoodsEntity outputGoods, int produceAmount) {
            InputGoods = null;
            InputAmount = 0;
            OutputGoods = outputGoods;
            ProduceAmount = produceAmount;
        }

        public ProduceAbility(GoodsEntity inputGoods, int inputAmount, GoodsEntity outputGoods, int produceAmount) {
            InputGoods = inputGoods;
            InputAmount = inputAmount;
            OutputGoods = outputGoods;
            ProduceAmount = produceAmount;
        }
    }
}
