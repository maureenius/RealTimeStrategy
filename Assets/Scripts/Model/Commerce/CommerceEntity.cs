using System.Collections.Generic;
using System.Linq;
using Model.Goods;
using Model.Route;
using Model.Town;
using Model.Territory;

#nullable enable

namespace Model.Commerce
{
    public class CommerceEntity
    {
        public readonly TownEntity Town;
        private readonly List<IRoute> _routes;
        private List<TerritoryRate> _monopolyRate;
        private const double CommerceScale = 1f;
        private readonly List<(IRoute route, Tension tension)> _actingTensions = new List<(IRoute route, Tension tension)>();

        public CommerceEntity(TownEntity town, IEnumerable<TerritoryEntity> territories)
        {
            this.Town = town;
            var territoryEntities = territories.ToList();
            _monopolyRate = new List<TerritoryRate>(territoryEntities.Select(ter => new TerritoryRate(ter, 0f)));
            if (town.IsCapital)
            {
                var territory = territoryEntities.First(ter => ter.IsOwn(town));
                _monopolyRate.First(rate => rate.Territory == territory).Rate = 1f;
            }
            
            _routes = RouteLayer.GetInstance().GetConnectedRoutes(town);
            _routes.ForEach(route =>
            {
                var tensions = territoryEntities.Select(ter => new Tension(ter, 0f)).ToList();
                route.UpdateTensions(tensions);
                _actingTensions.AddRange(tensions.Select(ten =>
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

        public void Monopolize(TerritoryEntity territory)
        {
            foreach (var territoryRate in _monopolyRate)
            {
                territoryRate.Rate = territoryRate.Territory == territory ? 1 : 0;
            }
        }

        private void Export()
        {
            var storages = GetExportableGoods().ToList();
            var targetRoutes = OutwardRoutes().ToList();
            var totalPower = targetRoutes.Sum(route => route.FlowPower());

            foreach (var storage in storages)
            {
                foreach (var route in targetRoutes)
                {
                    var amount = (int)(storage.Amount.Volume * (route.FlowPower() / totalPower));
                    var cargo = new Cargo(storage.Goods, amount);
                    storage.Amount.Consume(amount);

                    route.PushCargo(cargo);
                }
            }
            
            Town.CarryIn(storages);
        }

        private IEnumerable<Cargo> GetExportableGoods()
        {
            return Town.Storages.PickUpAll();
        }

        private IEnumerable<IRoute> OutwardRoutes()
        {
            return _routes.Where(r => (r.From == Town && r.FlowPower() > 0) || 
                                     (r.To == Town && r.FlowPower() < 0));
        }

        private void Import()
        {
            foreach (var route in _routes)
            {
                Town.CarryIn(route.TakeCargo());
            }
        }

        private void UpdateTension()
        {
            _monopolyRate.ForEach(rate =>
            {
                var power = rate.Rate * CommerceScale;
                var tensions = _actingTensions.Where(at => at.tension.Territory == rate.Territory).ToList();
                tensions.ForEach(t => t.tension.Power = power);
            });
        }
    }
}