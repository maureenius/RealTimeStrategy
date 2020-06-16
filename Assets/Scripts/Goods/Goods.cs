using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Assets.Scripts.Goods {
    public enum GoodsType {
        [Description("小麦")]
        FLOUR,
        [Description("砂糖")]
        SUGAR,
        [Description("菓子")]
        COOKIE
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

        public GoodsEntity(GoodsType _goodsType, string _name) {
            GoodsType = _goodsType;
            Name = _name;
        }
    }

    public class GeneralGoods : GoodsEntity {
        public GeneralGoods(GoodsType _goodsType, string _name) : base(_goodsType, _name) { }
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
