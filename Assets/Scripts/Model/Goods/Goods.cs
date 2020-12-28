using System.ComponentModel;
using Database;
using Model.Util;

#nullable enable

namespace Model.Goods {
    public abstract class GoodsEntity : INamed
    {
        private readonly GoodsData _baseData;
        public string SystemName { get; }
        public string DisplayName { get; }

        protected GoodsEntity(GoodsData data)
        {
            _baseData = data;
            SystemName = _baseData.Name;
            DisplayName = _baseData.DisplayName;
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
