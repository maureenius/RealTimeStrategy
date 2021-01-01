using System;
using Model.Territory;
using UniRx;
using UnityEngine;
using View;

#nullable enable

namespace Presenter
{
    public class MyTerritoryPresenter : MonoBehaviour
    {
        [SerializeField] private HeaderView? headerView;
        private TerritoryEntity? _myTerritoryEntity;

        public void Initialize(TerritoryEntity territory)
        {
            _myTerritoryEntity = territory;

            territory.OnChangeFaithPoint
                .Subscribe(_ => UpdateFaithPoint())
                .AddTo(this);
        }

        private void UpdateFaithPoint()
        {
            if (headerView == null || _myTerritoryEntity == null) throw new NullReferenceException();
            
            headerView.UpdateFaithPoint(_myTerritoryEntity.FaithPoint);
        }
    }
}