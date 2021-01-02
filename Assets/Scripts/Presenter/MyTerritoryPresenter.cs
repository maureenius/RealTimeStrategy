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
        [SerializeField] private MyTerritoryDetailView? myTerritoryDetailView;
        private TerritoryEntity? _myTerritoryEntity;

        public void Initialize(TerritoryEntity territory)
        {
            _myTerritoryEntity = territory;

            territory.OnChangeFaithPoint
                .Subscribe(_ => UpdateFaithPoint())
                .AddTo(this);

            territory.OnChangeMoney
                .Subscribe(_ => UpdateMoney())
                .AddTo(this);

            if (myTerritoryDetailView == null) throw new NullReferenceException();
            myTerritoryDetailView.OnCommandToMoney
                .Subscribe(item => CommandFaithToMoney(item.faith, item.money))
                .AddTo(this);
        }

        private void UpdateFaithPoint()
        {
            if (headerView == null || _myTerritoryEntity == null) throw new NullReferenceException();
            
            headerView.UpdateFaithPoint(_myTerritoryEntity.FaithPoint.Volume);
        }

        private void UpdateMoney()
        {
            if (headerView == null || _myTerritoryEntity == null) throw new NullReferenceException();
            
            headerView.UpdateMoney(_myTerritoryEntity.Money.Volume);
        }

        private void CommandFaithToMoney(float faith, float money)
        {
            if (_myTerritoryEntity == null) throw new NullReferenceException();
            
            _myTerritoryEntity.FaithToMoney(faith, money);
        }
    }
}