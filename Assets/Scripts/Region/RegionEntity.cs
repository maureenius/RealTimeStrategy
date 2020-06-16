using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Town;

namespace Assets.Scripts.Region {
    public abstract class RegionEntity {
        private IList<TownEntity> towns = new List<TownEntity>();
        public string Name { get; private set; }

        public RegionEntity(string _name) {
            Name = _name;
        }

        public void AttachTowns(TownEntity attachedTown)
        {
            towns.Add(attachedTown);
        }
        
        public void AttachTowns(IEnumerable<TownEntity> attachedTowns)
        {
            towns = towns.Union(attachedTowns).ToList();
        }
    }

    class GeneralRegion : RegionEntity {
        public GeneralRegion(string name) : base(name) { }
    }

    public static class RegionFactory {
        public static RegionEntity Create(string _name) {
            return new GeneralRegion(_name);
        }
    }
}
