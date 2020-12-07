﻿using System;
using System.Collections.Generic;
using System.Linq;
using Model.Town;
using Model.Util;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using View;

namespace Presenter
{
    public class TownsPresenter : MonoBehaviour
    {
        [SerializeField] private Transform townRoot;
        
        public IReadOnlyReactiveProperty<int> SelectedTownId => _selectedTownId;
        private IReactiveProperty<int> _selectedTownId = new IntReactiveProperty();
        private List<TownEntity> _entities;
        private List<TownView> _views;
        
        private readonly Subject<int> _outlineChangeSubject = new Subject<int>();
        public IObservable<int> OnOutlineChanged => _outlineChangeSubject;
        public IObservable<int> OnDetailChanged => _outlineChangeSubject;

        public void Initialize(IEnumerable<TownEntity> entities)
        {
            _entities = new List<TownEntity>(entities);
            _entities.ForEach(InitializeTown);

            _views = townRoot.GetComponentsInChildren<TownView>().ToList();
            _views.ForEach(view =>
            {
                view.OnSelected
                    .Subscribe(_ => OnSelected(view))
                    .AddTo(this);
            });
        }

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
        }

        private void InitializeTown(TownEntity town)
        {
            town.OnTurnPassed
                .Where(_ => town.Id == _selectedTownId.Value)
                .Subscribe(_ => _outlineChangeSubject.OnNext(town.Id))
                .AddTo(this);
        }
    }
}