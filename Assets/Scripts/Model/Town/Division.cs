using System;
using System.Collections.Generic;
using Model.Town.Building;
using Model.Town.Terrain;

#nullable enable

namespace Model.Town {
    public class Division {
        private readonly Guid _id;
        private ITerrain _terrain;
        private IBuildable? Building { get; set; }

        public Division(TerrainType terrainType)
        {
            _id = Guid.NewGuid();
            _terrain = terrainType switch
            {
                TerrainType.Plain => Plain.GetInstance(),
                _ => throw new InvalidOperationException()
            };
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
