using System.Collections.Generic;
using Model.Town;

#nullable enable

namespace Model.Region {
    public abstract class RegionEntity {
        private readonly IList<TownEntity> _towns = new List<TownEntity>();
        public string Name { get; }

        protected RegionEntity(string name) {
            Name = name;
        }

        public void AttachTowns(TownEntity attachedTown)
        {
            _towns.Add(attachedTown);
        }
    }

    class GeneralRegion : RegionEntity {
        public GeneralRegion(string name) : base(name) { }
    }

    public static class RegionFactory {
        public static RegionEntity Create(string name) {
            return new GeneralRegion(name);
        }
    }
}
