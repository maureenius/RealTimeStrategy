using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Global;
using Assets.Scripts.Goods;
using Assets.Scripts.Town;
using Assets.Scripts.Town.Building;
using Assets.Scripts.Territory.Command;

namespace Assets.Scripts.Territory {
    public abstract class TerritoryEntity {
        protected IList<TownEntity> towns = new List<TownEntity>();
        public string Name { get; private set; }
        protected IList<IBuildable> buildingTemplates = new List<IBuildable>();
        protected IList<ICommand> reservedCommands = new List<ICommand>();

        public TerritoryEntity(string name) {
            Name = name;
        }

        public void AttachTowns(TownEntity attachedTown)
        {
            towns.Add(attachedTown);
        }
        
        public void AttachTowns(IEnumerable<TownEntity> attachedTowns)
        {
            towns = towns.Union(attachedTowns).ToList();
        }

        public abstract void InitializeTowns();

        public void AddBuildingTemplate(IBuildable template) {
            if (buildingTemplates.FirstOrDefault(t => t.Name == template.Name) == null)
            {
                buildingTemplates.Add((IBuildable)template.Clone());
            }
        }

        public void DoOneTurn()
        {
            towns.ToList().ForEach(town => town.DoOneTurn());
        }
    }

    class Territory : TerritoryEntity {
        public Territory(string name) : base(name) {
            InitializeBuildingTemplate();
        }

        private void InitializeBuildingTemplate()
        {
            AddBuildingTemplate(Farm());
        }
        
        public override void InitializeTowns()
        {
            towns.ToList().ForEach(town =>
            {
                town.Build(buildingTemplates.First(template => template.Name == "農場"));
            });
        }

        private SimpleProducer Farm()
        {
            var pa = new ProduceAbility(GlobalGoods.GetInstance().FindByName("普通の小麦"), 3);
            return new SimpleProducer("農場", pa, 5);
        }
    }

    public static class TerritoryFactory {
        public static TerritoryEntity Create(string _name) {
            return new Territory(_name);
        }
    }
}
