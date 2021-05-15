using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using Model.Pops;
using Model.Town.Building;

#nullable enable

namespace Model.Town {
    public class Division : IDivision {
        public Guid Id { get; }
        public string TerrainName => _terrain.Name;
        private ITerrainData _terrain;
        public IBuildable? Building { get; private set; }

        public Division(ITerrainData terrain)
        {
            Id = Guid.NewGuid();
            _terrain = terrain;
        }

        public bool CanBuild(IBuildable target)
        {
            return (target.BuildableTerrainTypes.Contains(_terrain.TerrainName)) && (Building == null);
        }

        public void Build(IBuildable target) {
            if (!CanBuild(target)) throw new InvalidOperationException("建設不可能な地形にbuildしました");
            Building = target;
        }

        public void Demolish() {
            Building = null;
        }

        public IEnumerable<Workplace> ProvidedWorkplaces()
        {
            return Building == null ? new List<Workplace>() : Building.Workplaces;
        }
    }
}
