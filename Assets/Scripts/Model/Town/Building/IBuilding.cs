using System;
using System.Collections.Generic;
using Model.Goods;
using Model.Race;
using Model.Town.Terrain;
using Model.Util;

#nullable enable

namespace Model.Town.Building {
    public interface IBuildable : INamed
    {
        Guid Id { get; }
        IEnumerable<TerrainType> BuildableTerrainTypes { get; }
        string TypeName { get; }
        ICollection<Workplace> Workplaces { get; }

        public IBuildable Clone();
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
