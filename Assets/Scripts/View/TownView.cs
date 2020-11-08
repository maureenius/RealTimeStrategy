using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace View
{
    public class TownView : MonoBehaviour
    {
        [SerializeField] public int townId;
        
        private readonly Subject<Unit> _onSelectedSubject = new Subject<Unit>();
        public IObservable<Unit> OnSelected => _onSelectedSubject;

        public void OnClicked(BaseEventData e)
        {
            _onSelectedSubject.OnNext(Unit.Default);
        }
    }
}