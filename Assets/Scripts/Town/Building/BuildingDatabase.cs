﻿﻿using System.Collections.Generic;
 using Assets.Scripts.Global;
using Assets.Scripts.Goods;
 using Assets.Scripts.Race;

 namespace Assets.Scripts.Town.Building
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
                new ConsumptionTrait(GoodsType.FLOUR, 1),
                new ConsumptionTrait(GoodsType.SUGAR, 1)
            };
            return new SimpleProcessor("菓子工房", pa, ct,2);
        }
    }
}