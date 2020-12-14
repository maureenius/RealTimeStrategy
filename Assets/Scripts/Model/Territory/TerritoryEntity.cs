using System.Collections.Generic;
using System.Linq;
using Model.Goods;
using Model.Territory.Command;
using Model.Town;
using Model.Town.Building;

namespace Model.Territory {
    public abstract class TerritoryEntity {
        protected IList<TownEntity> towns = new List<TownEntity>();
        public string Name { get; private set; }
        protected IList<IBuildable> buildingTemplates = new List<IBuildable>();
        protected IList<ICommand> reservedCommands = new List<ICommand>();
        protected IList<ExportPlan> exportPlans = new List<ExportPlan>();

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

        public Util.Util.StatusCode AddExportPlan(ExportPlan plan)
        {
            if (!towns.Contains(plan.Sender)) return Util.Util.StatusCode.FAIL;
            return Util.Util.StatusCode.SUCCESS;
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
            AddBuildingTemplate(BuildingDatabase.FlourFarm());
            AddBuildingTemplate(BuildingDatabase.SugarCaneField());
            AddBuildingTemplate(BuildingDatabase.Confectionery());
        }
        
        public override void InitializeTowns()
        {
            towns.ToList().ForEach(town =>
            {
                town.Build(buildingTemplates.First(template => template.Name == "小麦農場"));
                town.Build(buildingTemplates.First(template => template.Name == "さとうきび畑"));
                town.Build(buildingTemplates.First(template => template.Name == "菓子工房"));
            });
        }
    }

    public struct ExportPlan
    {
        public TownEntity Sender { get; }
        public TownEntity Receiver { get; }
        public GoodsEntity Goods { get; }
        public int Amount { get; }
        
        public ExportPlan(TownEntity sender, TownEntity receiver, GoodsEntity goods, int amount)
        {
            Sender = sender;
            Receiver = receiver;
            Goods = goods;
            Amount = amount;
        }
    }

    public static class TerritoryFactory {
        public static TerritoryEntity Create(string _name) {
            return new Territory(_name);
        }
    }

    public struct TerritoryRate
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
