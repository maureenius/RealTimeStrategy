using System;
using System.Collections.Generic;
using System.Linq;
using Model.Goods;

#nullable enable

namespace Model.Global {
    public sealed class GlobalGoods {
        private static readonly GlobalGoods Instance = new GlobalGoods();
        
        private readonly List<GoodsEntity> closedGoodsList; 

        public static GlobalGoods GetInstance() {
            return Instance;
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
        
        public void Clear()
        {
            closedGoodsList.Clear();
        }

        private GlobalGoods() {
            closedGoodsList = new List<GoodsEntity>();
        } 
    }
}
