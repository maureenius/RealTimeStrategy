using System.Collections.Generic;
using System.Linq;
using Model.Town;

namespace Model.Route {
    public class RouteLayer {
        private IList<IRoute> routes = new List<IRoute>();

        public void DoOneTurn()
        {
            routes.ToList().ForEach(route => route.DoOneTurn());
        }
        public void Connect(TownEntity start, TownEntity end, int capacity, int length) {
            var addRoute = new Model.Route.Route(capacity, length, start, end);
            routes.Add(addRoute);
            // start.SetRoute(addRoute, isImport: false);
            // end.SetRoute(addRoute, isImport: true);
        }

        public void Disconnect(IRoute route)
        {
            // route.Sender.RemoveRoute(route, true);
            // route.Receiver.RemoveRoute(route, false);
            routes.Remove(route);
        }
    }
}
