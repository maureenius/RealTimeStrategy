using System.Collections.Generic;
using System.Linq;
using Model.Goods;
using Model.Route;
using Model.Town;
using Model.Territory;

namespace Model.Commerce
{
    public class CommerceEntity
    {
        private readonly TownEntity town;
        private readonly List<IRoute> routes;
        private List<TerritoryRate> monopolyRate;
        private double CommerceScale = 1f;
        private List<(IRoute route, Tension tension)> ActingTensions = new List<(IRoute route, Tension tension)>();

        public CommerceEntity(TownEntity town, IEnumerable<TerritoryEntity> territories)
        {
            this.town = town;
            var territoryEntities = territories.ToList();
            monopolyRate = new List<TerritoryRate>(territoryEntities.Select(ter => new TerritoryRate(ter, 0f)));
            if (town.IsCapital)
            {
                var territory = territoryEntities.First(ter => ter.IsOwn(town));
                monopolyRate.First(rate => rate.Territory == territory).Rate = 1f;
            }
            
            routes = RouteLayer.GetInstance().GetConnectedRoutes(town);
            routes.ForEach(route =>
            {
                var tensions = territoryEntities.Select(ter => new Tension(ter, 0f)).ToList();
                route.UpdateTensions(tensions);
                ActingTensions.AddRange(tensions.Select(ten =>
                {
                    (IRoute r, Tension tension) tmp = (route, ten);
                    return tmp;
                }));
            });
        }

        public void DoOneTurn()
        {
            Export();
            Import();
            UpdateTension();
        }

        private void Export()
        {
            var storages = GetExportableGoods();
            var targetRoutes = OutwardRoutes().ToList();
            var totalPower = targetRoutes.Sum(route => route.FlowPower());

            foreach (var storage in storages)
            {
                foreach (var route in targetRoutes)
                {
                    var amount = (int)(storage.Amount * (route.FlowPower() / totalPower));
                    var cargo = new Cargo(storage.Goods, amount);
                    storage.Consume(amount);

                    route.PushCargo(cargo);
                }
            }
        }

        private IEnumerable<Storage> GetExportableGoods()
        {
            return town.Storages;
        }

        private IEnumerable<IRoute> OutwardRoutes()
        {
            return routes.Where(r => (r.From == town && r.FlowPower() > 0) || 
                                     (r.To == town && r.FlowPower() < 0));
        }

        private void Import()
        {
            town.CarryIn(routes.Select(route => route.TakeCargo()));
        }

        private IEnumerable<IRoute> InwardRoutes()
        {
            return routes.Where(r => (r.From == town && r.FlowPower() < 0 || 
                                     (r.To == town && r.FlowPower() > 0)));
        }

        private void UpdateTension()
        {
            monopolyRate.ForEach(rate =>
            {
                var power = rate.Rate * CommerceScale;
                var tensions = ActingTensions.Where(at => at.tension.Territory == rate.Territory).ToList();
                tensions.ForEach(t => t.tension.Power = power);
            });
        }
    }
}