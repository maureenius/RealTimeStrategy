using System;
using System.Collections.Generic;
using Database;
using Model.Goods;
using Model.Race;
using Model.Util;
using Model.Pops;

#nullable enable

namespace Model.Town.Building {
    public interface IBuildable : INamed
    {
        Guid Id { get; }
        IEnumerable<TerrainName> BuildableTerrainTypes { get; }
        IEnumerable<Workplace> Workplaces { get; }

        public IBuildable Clone();
    }
}
