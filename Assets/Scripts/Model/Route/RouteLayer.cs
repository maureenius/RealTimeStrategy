using System.Collections.Generic;
using System.Linq;
using Model.Town;

namespace Model.Route {
    // シングルトンクラス
    public sealed class RouteLayer {
        private static RouteLayer _instance = new RouteLayer();

        public static RouteLayer GetInstance()
        {
            return _instance;
        }

        private RouteLayer() {}
        
        private IList<IRoute> routes = new List<IRoute>();

        public void DoOneTurn()
        {
            routes.ToList().ForEach(route => route.DoOneTurn());
        }
        
        public void Connect(TownEntity start, TownEntity end, int capacity, int length) {
            var addRoute = new Route(capacity, length, start, end);
            routes.Add(addRoute);
        }

        public void Disconnect(IRoute route)
        {
            routes.Remove(route);
        }

        public List<IRoute> GetConnectedRoutes(TownEntity town)
        {
            return routes
                .Where(route => route.From == town || route.To == town)
                .ToList();
        }
    }
}
