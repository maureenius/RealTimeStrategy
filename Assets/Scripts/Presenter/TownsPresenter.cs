using System;
using System.Collections.Generic;
using System.Linq;
using Model.Town;
using UniRx;
using UnityEngine;
using View;

#nullable enable

namespace Presenter
{
    public class TownsPresenter : MonoBehaviour
    {
        [SerializeField] private Transform? townRoot;
        
        public IReadOnlyReactiveProperty<int> SelectedTownId => _selectedTownId;
        private readonly IReactiveProperty<int> _selectedTownId = new IntReactiveProperty();
        private List<TownEntity> _entities = new List<TownEntity>();
        private List<TownView> _views = new List<TownView>();
        
        private readonly Subject<int> _outlineChangeSubject = new Subject<int>();
        public IObservable<int> OnOutlineChanged => _outlineChangeSubject;
        public IObservable<int> OnDetailChanged => _outlineChangeSubject;

        public TownEntity FindEntityById(int id)
        {
            return _entities.First(entity => entity.Id == id);
        }

        public TownView FindViewById(int id)
        {
            return _views.First(view => view.townId == id);
        }

        private void OnSelected(TownView view)
        {
            _selectedTownId.Value = view.townId;
            foreach (var townView in _views.Where(v => v != view))
            {
                townView.OnUnselected();
            }
        }

        public void InitializeView()
        {
            if (townRoot == null) throw new NullReferenceException();
             
            foreach (var view in townRoot.GetComponentsInChildren<TownView>())
            {
                _views.Add(view);
                
                view.OnSelected
                    .Subscribe(_ => OnSelected(view))
                    .AddTo(this);
            }
        }

        public void InitializeEntity(TownEntity town)
        {
            _entities.Add(town);
            
            town.OnTurnPassed
                .Where(_ => town.Id == _selectedTownId.Value)
                .Subscribe(_ => _outlineChangeSubject.OnNext(town.Id))
                .AddTo(this);
        }
    }
}