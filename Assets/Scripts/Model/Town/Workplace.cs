using System;
using System.Collections.Generic;
using System.Linq;
using Model.Goods;
using Model.Race;
using Model.Town.Building;
using Model.Util;

#nullable enable

namespace Model.Town
{
    public interface IWorkplace : INamed, IHasProduceAbility, IHasConsumptionTrait
    {
        public Guid Id { get; }
    }
    
    public class Workplace : IWorkplace
    {
        public Guid Id { get; }
        public string SystemName { get; }
        public string DisplayName { get; }
        public IEnumerable<ProduceAbility> ProduceAbilities { get; }
        public IEnumerable<ConsumptionTrait> ConsumptionTraits { get; }

        public Workplace(Database.WorkplaceData data)
        {
            Database.WorkplaceData baseData = data;
            
            Id = Guid.NewGuid();
            SystemName = baseData.Name;
            DisplayName = baseData.DisplayName;

            ProduceAbilities = baseData.Products
                .Select(goods => new ProduceAbility(GlobalGoods.GetInstance().FindByName(goods.Goods.Name),
                    goods.amount));

            ConsumptionTraits = baseData.Consumptions
                .Select(goods => new ConsumptionTrait(GlobalGoods.GetInstance().FindByName(goods.Goods.Name),
                    goods.amount));
        }
    }
}