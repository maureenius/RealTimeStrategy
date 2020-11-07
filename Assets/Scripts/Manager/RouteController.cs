using Model.Route;
using UnityEngine;

namespace Manager
{
    public class RouteController : MonoBehaviour
    {
        public IRoute routeModel;

        public void Initialize(IRoute route) {
            routeModel = route;
        }
    }
}
