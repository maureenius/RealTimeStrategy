using System.Collections.Generic;
using System.Linq;
using Model.Goods;
using Model.Territory.Command;
using Model.Town;
using Model.Town.Building;

namespace Model.Territory {
    public abstract class TerritoryEntity {
        protected IList<TownEntity> Towns = new List<TownEntity>();
        public string Name { get; private set; }
        protected readonly IList<IBuildable> BuildingTemplates = new List<IBuildable>();
        protected IList<ICommand> ReservedCommands = new List<ICommand>();
        protected IList<ExportPlan> ExportPlans = new List<ExportPlan>();

        public TerritoryEntity(string name) {
            Name = name;
        }

        public void AttachTowns(TownEntity attachedTown)
        {
            Towns.Add(attachedTown);
        }
        
        public void AttachTowns(IEnumerable<TownEntity> attachedTowns)
        {
            Towns = Towns.Union(attachedTowns).ToList();
        }

        public Util.Util.StatusCode AddExportPlan(ExportPlan plan)
        {
            if (!Towns.Contains(plan.Sender)) return Util.Util.StatusCode.Fail;
            return Util.Util.StatusCode.Success;
        }

        public abstract void InitializeTowns();

        public void AddBuildingTemplate(IBuildable template) {
            if (BuildingTemplates.FirstOrDefault(t => t.Name == template.Name) == null)
            {
                BuildingTemplates.Add((IBuildable)template.Clone());
            }
        }

        public void DoOneTurn()
        {
            Towns.ToList().ForEach(town => town.DoOneTurn());
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
            AddBuildingTemplate(BuildingDatabase.FlourFarm());
            AddBuildingTemplate(BuildingDatabase.SugarCaneField());
            AddBuildingTemplate(BuildingDatabase.Confectionery());
        }
        
        public override void InitializeTowns()
        {
            Towns.ToList().ForEach(town =>
            {
                town.Build(BuildingTemplates.First(template => template.Name == "小麦農場"));
                town.Build(BuildingTemplates.First(template => template.Name == "さとうきび畑"));
                town.Build(BuildingTemplates.First(template => template.Name == "菓子工房"));
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
