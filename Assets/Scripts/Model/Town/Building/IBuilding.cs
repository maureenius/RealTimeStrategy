using System;
using System.Collections.Generic;
using Database;
using Model.Goods;
using Model.Race;
using Model.Util;

#nullable enable

namespace Model.Town.Building {
    public interface IBuildable : INamed
    {
        Guid Id { get; }
        IEnumerable<TerrainName> BuildableTerrainTypes { get; }
        IEnumerable<Workplace> Workplaces { get; }

        public IBuildable Clone();
    }

    public interface IHasProduceAbility
    {
        IEnumerable<ProduceAbility> ProduceAbilities { get; }
    }

    public interface IHasConsumptionTrait
    {
        IEnumerable<ConsumptionTrait> ConsumptionTraits { get; }
    }
}
