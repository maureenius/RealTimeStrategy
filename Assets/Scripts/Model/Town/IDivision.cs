using System;
using System.Collections.Generic;
using Model.Town.Building;

#nullable enable

namespace Model.Town
{
    public interface IDivision
    {
        public Guid Id { get; }
        public string TerrainName { get; }
        public IBuildable? Building { get; }
        public bool CanBuild(IBuildable target);
        public void Build(IBuildable target);
        public void Demolish();
        public IEnumerable<IWorkplace> ProvidedWorkplaces();
    }
}