using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Town;
using Assets.Scripts.Town.Building;
using Assets.Scripts.Territory.Command;

namespace Assets.Scripts.Territory {
    public abstract class TerritoryEntity {
        private IList<TownEntity> towns = new List<TownEntity>();
        public string Name { get; private set; }
        private IList<IBuildable> buildingTemplates = new List<IBuildable>();
        private IList<ICommand> reservedCommands = new List<ICommand>();

        public TerritoryEntity(string _name) {
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

        public Util.Util.StatusCode AddBuildingTemplate(IBuildable template) {
            if (buildingTemplates.FirstOrDefault(t => t.Name == template.Name) != null) return Util.Util.StatusCode.FAIL;
            buildingTemplates.Add((IBuildable)template.Clone());

            return Util.Util.StatusCode.SUCCESS;
        }

        public void DoOneTurn()
        {
            towns.ToList().ForEach(town => town.DoOneTurn());
        }
    }

    class Territory : TerritoryEntity {
        public Territory(string _name) : base(_name) {

        }
    }

    public static class TerritoryFactory {
        public static TerritoryEntity Create(string _name) {
            return new Territory(_name);
        }
    }
}
