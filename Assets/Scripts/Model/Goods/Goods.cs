using Database;
using Model.Util;

#nullable enable

namespace Model.Goods {
    public abstract class GoodsEntity : INamed
    {
        public string SystemName { get; }
        public string DisplayName { get; }

        protected GoodsEntity(GoodsData data)
        {
            GoodsData baseData = data;
            SystemName = baseData.Name;
            DisplayName = baseData.DisplayName;
        }
    }

    public class GeneralGoods : GoodsEntity {
        public GeneralGoods(GoodsData data) : base(data) { }
    }

    public static class GoodsFactory {
        public static GoodsEntity Create(GoodsData data) {
            return new GeneralGoods(data);
        }
    }
}
