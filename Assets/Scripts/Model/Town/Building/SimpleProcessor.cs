using System;
using System.Collections.Generic;
using Model.Goods;
using Model.Race;
using Model.Town.Terrain;

#nullable enable

namespace Model.Town.Building
{
    public class SimpleProcessor : IBuildable, IHasProduceAbility, IHasConsumptionTrait
    {
        public Guid Id { get; }
        public IEnumerable<TerrainType> BuildableTerrainTypes { get; }
        public string Name { get; }
        public string TypeName { get; }
        public ICollection<Workplace> Workplaces { get; }
        public IList<ProduceAbility> ProduceAbilities { get; }
        public IList<ConsumptionTrait> ConsumptionTraits { get; }

        public SimpleProcessor(string name, ProduceAbility produceAbility, IEnumerable<ConsumptionTrait> consumptionTraits,
            int slotNum, IEnumerable<TerrainType> buildableTerrainTypes)
        {
            Id = Guid.NewGuid();
            Name = name;
            TypeName = name;
            ProduceAbilities = new List<ProduceAbility>{produceAbility};
            ConsumptionTraits = new List<ConsumptionTrait>(consumptionTraits);

            Workplaces = new List<Workplace>();
            for (var i = 0; i < slotNum; i++)
            {
                Workplaces.Add(new Workplace(this));
            }

            BuildableTerrainTypes = buildableTerrainTypes;
        }

        public IBuildable Clone()
        {
            return (SimpleProcessor)MemberwiseClone();
        }
    }
}