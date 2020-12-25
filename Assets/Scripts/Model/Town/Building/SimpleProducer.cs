using System;
using System.Collections.Generic;
using System.Linq;
using Model.Goods;
using Model.Town.Terrain;

#nullable enable

namespace Model.Town.Building {
    public class SimpleProducer : IBuildable, IHasProduceAbility {
        public Guid Id { get; }
        public IEnumerable<TerrainType> BuildableTerrainTypes { get; }
        public string Name { get; }
        public string TypeName { get; }
        public ICollection<Workplace> Workplaces { get; }
        public IList<ProduceAbility> ProduceAbilities { get; }

        public SimpleProducer(string name, ProduceAbility produceAbility, int slotNum,
            IEnumerable<TerrainType> buildableTerrainTypes) {
            Id = Guid.NewGuid();
            Name = name;
            TypeName = name;
            ProduceAbilities = new List<ProduceAbility>{produceAbility};

            Workplaces = new List<Workplace>();
            for (var i = 0; i < slotNum; i++)
            {
                Workplaces.Add(new Workplace(this));
            }

            BuildableTerrainTypes = buildableTerrainTypes;
        }

        public IBuildable Clone() {
            return (SimpleProducer)MemberwiseClone();
        }
    }
}
