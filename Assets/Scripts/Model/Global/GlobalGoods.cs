using System;
using System.Collections.Generic;
using System.Linq;
using Model.Goods;

namespace Model.Global {
    public sealed class GlobalGoods {
        private static readonly GlobalGoods _globalGoods = new GlobalGoods();
        
        private List<GoodsEntity> closedGoodsList; 

        public static GlobalGoods GetInstance() {
            return _globalGoods;
        }

        public void Register(GoodsEntity goods) {
            if (closedGoodsList.FirstOrDefault(g => g.Name == goods.Name) != null) {
                throw new InvalidOperationException("同名のGoodsが登録されました");
            }

            closedGoodsList.Add(GoodsFactory.Copy(goods));
        }

        public GoodsEntity FindByName(string name){
            return GoodsFactory.Copy(closedGoodsList.Find(g => g.Name == name));
        }

        private GlobalGoods() {
            closedGoodsList = new List<GoodsEntity>();
        } 
    }
}
