using System;
using System.Collections.Generic;
using System.Linq;
using Database;

#nullable enable

namespace Model.Town.Building {
    public class SimpleProducer : IBuildable
    {
        public Guid Id { get; }

        public string SystemName { get; }
        public string DisplayName { get; }

        public IEnumerable<TerrainName> BuildableTerrainTypes { get; }
        public IEnumerable<IWorkplace> Workplaces { get; }

        public SimpleProducer(BuildingData data) {
            BuildingData baseData = data;
            
            Id = Guid.NewGuid();
            SystemName = string.Copy(baseData.Name);
            DisplayName = string.Copy(baseData.DisplayName);
            BuildableTerrainTypes = new List<TerrainName>(baseData.BuildableTerrains);
            Workplaces = Enumerable.Range(0, baseData.Workplace.count)
                .Select(index => new Workplace(baseData.Workplace.Workplace))
                .Cast<IWorkplace>()
                .ToList();
        }

        public IBuildable Clone() {
            return (SimpleProducer)MemberwiseClone();
        }

        public BuildingData ToData()
        {
            throw new NotImplementedException();
        }
    }
}
