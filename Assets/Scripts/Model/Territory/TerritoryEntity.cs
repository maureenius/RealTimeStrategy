using System.Collections.Generic;
using System.Linq;
using Model.Town;
using Model.Town.Building;

#nullable enable

namespace Model.Territory {
    public abstract class TerritoryEntity {
        protected readonly IList<TownEntity> Towns = new List<TownEntity>();
        public string Name { get; }
        protected readonly IList<IBuildable> BuildingTemplates = new List<IBuildable>();

        protected TerritoryEntity(string name) {
            Name = name;
        }

        public void AttachTowns(TownEntity attachedTown)
        {
            Towns.Add(attachedTown);
        }

        public abstract void InitializeTowns();

        public void AddBuildingTemplate(IBuildable template) {
            if (BuildingTemplates.FirstOrDefault(t => t.SystemName == template.SystemName) == null)
            {
                BuildingTemplates.Add(template.Clone());
            }
        }

        public void DoOneTurn()
        {
            
        }

        public bool IsOwn(TownEntity town)
        {
            return Towns.Contains(town);
        }
    }

    class Territory : TerritoryEntity {
        public Territory(string name) : base(name) {
            InitializeBuildingTemplate();
        }

        private void InitializeBuildingTemplate()
        {
            AddBuildingTemplate(BuildingFactory.FlourFarm());
            AddBuildingTemplate(BuildingFactory.SugarCaneField());
            AddBuildingTemplate(BuildingFactory.Confectionery());
        }
        
        public override void InitializeTowns()
        {
            Towns.ToList().ForEach(town =>
            {
                town.Build(BuildingTemplates.First(template => template.SystemName == "小麦農場"));
                town.Build(BuildingTemplates.First(template => template.SystemName == "さとうきび畑"));
                town.Build(BuildingTemplates.First(template => template.SystemName == "菓子工房"));
            });
        }
    }

    public static class TerritoryFactory {
        public static TerritoryEntity Create(string name) {
            return new Territory(name);
        }
    }

    public class TerritoryRate
    {
        public readonly TerritoryEntity Territory;
        public double Rate;

        public TerritoryRate(TerritoryEntity territory, double rate)
        {
            Territory = territory;
            Rate = rate;
        }
    }
}
