using System;
using System.Linq;
using UniRx;
using UnityEngine;

#nullable enable

namespace View
{
    public class CameraView : MonoBehaviour
    {
        [SerializeField] private GameObject? townRoot;
        [SerializeField] private GameObject? routeRoot;
        
        public void Initialize()
        {
            if (townRoot == null || routeRoot == null) throw new NullReferenceException();
            
            foreach (var town in townRoot.GetComponentsInChildren<TownView>())
            {
                town.OnSelected.Subscribe(_ => Focus(town.gameObject.transform)).AddTo(this);
            }

            foreach (var route in routeRoot.GetComponentsInChildren<RouteView>())
            {
                route.OnSelected.Subscribe(_ => Focus(route.gameObject.transform)).AddTo(this);
            }
        }

        private void Focus(Transform target)
        {
            var thisTransform = transform;
            var from = thisTransform.position;
            var to = target.position;
            thisTransform.position = new Vector3(to.x, from.y, to.z);
        }
    }
}