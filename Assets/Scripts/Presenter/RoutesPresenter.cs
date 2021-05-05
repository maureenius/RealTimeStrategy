using System;
using System.Collections.Generic;
using System.Linq;
using Model.Route;
using UniRx;
using UnityEngine;
using View;

#nullable enable

namespace Presenter
{
    public class RoutesPresenter : MonoBehaviour
    {
        [SerializeField] private Transform? routeRoot;
        [SerializeField] private GameObject? routePrefab;
        [SerializeField] private GameObject? townRoot;

        public IReadOnlyReactiveProperty<Guid> SelectedRouteId => _selectedRouteId;
        private readonly IReactiveProperty<Guid> _selectedRouteId = new ReactiveProperty<Guid>();
        private List<IRoute> _models = new List<IRoute>();
        private List<RouteView> _views = new List<RouteView>();

        public void Initialize(IEnumerable<IRoute> models)
        {
            if(routeRoot == null || townRoot == null) throw new NullReferenceException();

            var routes = models.ToList();
            _models = new List<IRoute>(routes);
            
            var towns = townRoot.transform.GetComponentsInChildren<TownView>();
            _views = new List<RouteView>(routes.Select(route => CreateRoute(route, towns)));
        }

        private RouteView CreateRoute(IRoute route, IEnumerable<TownView> towns)
        {
            var obj = Instantiate(routePrefab, routeRoot);
            if(obj == null) throw new NullReferenceException();

            var view = obj.GetComponent<RouteView>();
            var townViews = towns.ToList();
            var fromTown = townViews.Single(t => t.townId == route.From.Id);
            var toTown = townViews.Single(t => t.townId == route.To.Id);
            
            view.Initialize(new RouteData(Guid.NewGuid(), fromTown.transform.position,
                toTown.transform.position, route.FlowPower()));
            view.OnSelected.Subscribe(_ => OnSelected(view)).AddTo(this);
            route.OnRecalculation
                .Where(_ => route.Id == _selectedRouteId.Value)
                .Subscribe(_ => view.UpdateFlowPower(route.FlowPower()))
                .AddTo(this);
            route.OnCargoChanged
                .Subscribe(items => view.UpdateCargoText(items))
                .AddTo(this);
            
            return view;
        }

        private void OnSelected(RouteView view)
        {
            foreach (var routeView in _views.Where(v => v != view))
            {
                routeView.OnUnselected();
            }
        }
    }
}