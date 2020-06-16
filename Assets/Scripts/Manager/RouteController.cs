using Assets.Scripts.Route;
using UnityEngine;

namespace Route
{
    public class RouteController : MonoBehaviour
    {
        public IRoute routeModel;

        public void Initialize(IRoute route) {
            routeModel = route;
        }
    }
}
