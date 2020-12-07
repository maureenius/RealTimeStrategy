using System;
using System.Collections.Generic;
using Model.Goods;
using Model.Race;
using Model.Util;

namespace Model.Town.Building {
    public interface IBuildable : INamed, ICloneable
    {
        // IEnumerable<TerrainEntity> buildableTerrain;
        int SlotNum { get; }
        string TypeName { get; }
        Workplace SlotTemplate { get; }
    }

    public interface IHasProduceAbility
    {
        IList<ProduceAbility> ProduceAbilities { get; }
    }

    public interface IHasConsumptionTrait
    {
        IList<ConsumptionTrait> ConsumptionTraits { get; }
    }
}
