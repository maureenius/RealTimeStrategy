using System.Collections.Generic;
using System.Linq;
using Model.Town;

#nullable enable

namespace Model.Route {
    // シングルトンクラス
    public sealed class RouteLayer {
        private static readonly RouteLayer Instance = new RouteLayer();

        public static RouteLayer GetInstance()
        {
            return Instance;
        }

        private RouteLayer() {}
        
        public IList<IRoute> Routes { get; } = new List<IRoute>();

        public void DoOneTurn()
        {
            Routes.ToList().ForEach(route => route.DoOneTurn());
        }
        
        public void Connect(TownEntity start, TownEntity end, int capacity, int length)
        {
            if (Routes.Any(route => route.From.Id == start.Id && route.To.Id == end.Id ||
                                    route.From.Id == end.Id && route.To.Id == start.Id)) return;
            
            var addRoute = new Route(capacity, length, start, end);
            Routes.Add(addRoute);
        }

        public void Disconnect(IRoute route)
        {
            Routes.Remove(route);
        }

        public List<IRoute> GetConnectedRoutes(TownEntity town)
        {
            return Routes
                .Where(route => route.From == town || route.To == town)
                .ToList();
        }
    }
}
