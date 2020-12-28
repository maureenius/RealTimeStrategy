using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using Model.Town.Building;

#nullable enable

namespace Model.Town {
    public class Division {
        private readonly Guid _id;
        private TerrainData _terrain;
        public IBuildable? Building { get; private set; }

        public Division(string terrainName)
        {
            _id = Guid.NewGuid();
            _terrain = TerrainDatabase.Find(terrainName);
        }

        public bool CanBuild(IBuildable target)
        {
            return (target.BuildableTerrainTypes.Contains(_terrain.TerrainName)) && (Building == null);
        }

        public void Build(IBuildable target) {
            if (Building != null) throw new InvalidOperationException("建物が存在する区画にbuildしました");
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
