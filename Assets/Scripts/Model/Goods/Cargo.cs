using Model.Util;

#nullable enable

namespace Model.Goods {
    public class Cargo {
        public GoodsEntity Goods { get; }
        public Tank Amount { get; }

        public Cargo(GoodsEntity goods, float amount) {
            Goods = goods;
            Amount = new Tank(amount);
        }

        public bool IsSameGoodsType(string typeName)
        {
            return Goods.TypeName == typeName;
        }
    }
}
