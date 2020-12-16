using System.Collections.Generic;
using Model.Global;
using Model.Goods;
using Model.Race;

namespace Model.Town.Building
{
    public static class BuildingDatabase
    {
        public static SimpleProducer FlourFarm()
        {
            var pa = new ProduceAbility(GlobalGoods.GetInstance().FindByName("普通の小麦"), 5);
            return new SimpleProducer("小麦農場", pa, 2);
        }
        
        public static SimpleProducer SugarCaneField()
        {
            var pa = new ProduceAbility(GlobalGoods.GetInstance().FindByName("普通の砂糖"), 3);
            return new SimpleProducer("さとうきび畑", pa, 2);
        }
        
        public static SimpleProcessor Confectionery()
        {
            var pa = new ProduceAbility(GlobalGoods.GetInstance().FindByName("普通のクッキー"), 3);
            var ct = new List<ConsumptionTrait>
            {
                new ConsumptionTrait(GoodsType.Flour, 1),
                new ConsumptionTrait(GoodsType.Sugar, 1)
            };
            return new SimpleProcessor("菓子工房", pa, ct,2);
        }
    }
}