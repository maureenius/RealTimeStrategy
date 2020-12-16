using System.ComponentModel;

namespace Model.Goods {
    public enum GoodsType {
        [Description("小麦")]
        Flour,
        [Description("砂糖")]
        Sugar,
        [Description("菓子")]
        Cookie
    }

    public static class GoodsTypeMethod {
        public static string GetNameJpn(this GoodsType goodsType) {
            var gm = goodsType.GetType().GetMember(goodsType.ToString());
            var attributes = gm[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return ((DescriptionAttribute)attributes[0]).Description;
        }
    }

    public abstract class GoodsEntity {
        public GoodsType GoodsType { get; private set; }
        public string Name { get; private set; }

        public GoodsEntity(GoodsType goodsType, string name) {
            GoodsType = goodsType;
            Name = name;
        }
    }

    public class GeneralGoods : GoodsEntity {
        public GeneralGoods(GoodsType goodsType, string name) : base(goodsType, name) { }
    }

    public static class GoodsFactory {
        public static GoodsEntity Create(GoodsType goodsType, string name) {
            return new GeneralGoods(goodsType, name);
        }

        public static GoodsEntity Copy(GoodsEntity original) {
            return new GeneralGoods(original.GoodsType, original.Name);
        }
    }
}
