using System;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

#nullable enable

namespace View
{
    public class RouteView : MonoBehaviour
    {
        private RouteData _route;

        [SerializeField] private GameObject? arrow;
        [SerializeField] private Canvas? cargoCanvas;
        [SerializeField] private TextMeshProUGUI? cargoText;
        [SerializeField] private string cameraName = "";
        
        private readonly Subject<Unit> _onSelectedSubject = new Subject<Unit>();
        public IObservable<Unit> OnSelected => _onSelectedSubject;

        public void Initialize(RouteData data)
        {
            if (cargoCanvas == null) throw new NullReferenceException();
            
            _route = data;
            cargoCanvas.worldCamera = GameObject.Find(cameraName).GetComponent<Camera>();
            
            transform.position = (_route.FromPos + _route.ToPos) / 2f;
            LookAtTo();
        }

        public void UpdateFlowPower(double power)
        {
            if (power >= 0)
            {
                LookAtTo();
            }
            else
            {
                LookAtFrom();
            }
        }

        public void OnClicked()
        {
            if (arrow == null) throw new NullReferenceException();
            
            arrow.GetComponent<SpriteRenderer>().color = Color.black;
            _onSelectedSubject.OnNext(Unit.Default);
        }

        public void OnUnselected()
        {
            if (arrow == null) throw new NullReferenceException();
            
            arrow.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0.5f);
        }

        public void UpdateCargoText(IEnumerable<string> cargoNames)
        {
            if (cargoText == null) throw new NullReferenceException();
            
            cargoText.text = $"{string.Join(",", cargoNames)}";
        }

        private void LookAtFrom()
        {
            if (arrow == null) throw new NullReferenceException();
            
            arrow.transform.Rotate(Vector3.forward, (float)(Math.Atan2(_route.FromPos.z - _route.ToPos.z,
                _route.FromPos.x - _route.ToPos.x) * 180f / Math.PI));
        }

        private void LookAtTo()
        {
            if (arrow == null) throw new NullReferenceException();
            
            arrow.transform.Rotate(Vector3.forward, (float)(Math.Atan2(_route.ToPos.z - _route.FromPos.z,
                _route.ToPos.x - _route.FromPos.x) * 180f / Math.PI));
        }
    }

    public readonly struct RouteData
    {
        public readonly Guid Id;
        public readonly Vector3 FromPos;
        public readonly Vector3 ToPos;
        public readonly double FlowPower;
        
        public RouteData(Guid id, Vector3 fromPos, Vector3 toPos, double flowPower)
        {
            Id = id;
            FromPos = fromPos;
            ToPos = toPos;
            FlowPower = flowPower;
        }
    }
}
