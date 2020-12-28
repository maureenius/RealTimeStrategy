using System;
using System.Collections.Generic;
using Database;

#nullable enable

namespace Model.Goods {
    public sealed class GlobalGoods {
        private static readonly GlobalGoods Instance = new GlobalGoods();
        
        private IDictionary<string, GoodsEntity> _closedGoodsList = new Dictionary<string, GoodsEntity>(); 

        public static GlobalGoods GetInstance() {
            return Instance;
        }

        public void Register(GoodsEntity goods) {
            if (_closedGoodsList.ContainsKey(goods.SystemName)) {
                throw new InvalidOperationException("同名のGoodsが登録されました");
            }

            _closedGoodsList.Add(goods.SystemName, goods);
        }

        public GoodsEntity FindByName(string name)
        {
            if (!_closedGoodsList.ContainsKey(name)) throw new KeyNotFoundException(name);
            
            return _closedGoodsList[name];
        }
        
        public void Initialize()
        {
            _closedGoodsList.Clear();

            foreach (var goodsData in GoodsDatabase.All())
            {
                var goods = GoodsFactory.Create(goodsData.Value);
                Register(goods);
            }
        }

        private GlobalGoods() {
            Initialize();
        } 
    }
}
