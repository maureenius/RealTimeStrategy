using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using QuickOutline;
using QuickOutline.Scripts;

namespace View
{
    public class TownView : MonoBehaviour
    {
        [SerializeField] public int townId;
        [SerializeField] public float outlineWidth;
        
        private readonly Subject<Unit> _onSelectedSubject = new Subject<Unit>();
        public IObservable<Unit> OnSelected => _onSelectedSubject;

        public void OnClicked(BaseEventData e)
        {
            GetComponent<Outline>().OutlineWidth = outlineWidth;
            _onSelectedSubject.OnNext(Unit.Default);
        }

        public void OnUnselected()
        {
            GetComponent<Outline>().OutlineWidth = 0f;
        }
    }
}