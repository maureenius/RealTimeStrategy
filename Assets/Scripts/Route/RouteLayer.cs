using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Town;

namespace Assets.Scripts.Route {
    public class RouteLayer {
        private IList<IRoute> routes = new List<IRoute>();

        public void DoOneTurn()
        {
            routes.ToList().ForEach(route => route.DoOneTurn());
        }
        public void Connect(TownEntity start, TownEntity end, int capacity, int length) {
            var addRoute = new Route(capacity, length);
            routes.Add(addRoute);
            start.SetRoute(addRoute, isImport: false);
            end.SetRoute(addRoute, isImport: true);
            
            routes.Add(addRoute);
        }

        public void Disconnect(IRoute route)
        {
            routes.Remove(route);
        }
    }
}
