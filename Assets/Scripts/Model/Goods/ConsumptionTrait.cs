using System;

# nullable enable

namespace Model.Goods
{
    public class ConsumptionTrait : IEquatable<ConsumptionTrait>
    {
        public string GoodsName { get; }
        public float Weight { get; }

        public ConsumptionTrait(
            GoodsEntity goods, 
            float weight
        ) {
            GoodsName = goods.SystemName;
            Weight = weight;
        }

        public bool Equals(ConsumptionTrait other)
        {
            return GoodsName == other.GoodsName && Math.Abs(Weight - other.Weight) < Util.Util.MIN;
        }
    }
}