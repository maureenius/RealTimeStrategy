using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using Model.Goods;

#nullable enable

namespace Model.Town.Building {
    public class SimpleProducer : IBuildable
    {
        private readonly BuildingData _baseData;
        public Guid Id { get; }

        public string SystemName { get; }
        public string DisplayName { get; }

        public IEnumerable<TerrainName> BuildableTerrainTypes { get; }
        public IEnumerable<Workplace> Workplaces { get; }

        public SimpleProducer(BuildingData data) {
            _baseData = data;
            
            Id = Guid.NewGuid();
            SystemName = string.Copy(_baseData.Name);
            DisplayName = string.Copy(_baseData.DisplayName);
            BuildableTerrainTypes = new List<TerrainName>(_baseData.BuildableTerrains);
            Workplaces = Enumerable
                .Range(0, _baseData.Workplace.count)
                .Select(_ => new Workplace(_baseData.Workplace.Workplace));
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
