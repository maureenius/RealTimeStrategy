using System;
using System.Collections.Generic;
using System.Linq;
using Model.Territory;
using Model.Town.Building;
using Store;
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
        [SerializeField] private BuildingTemplateView? buildingTemplateView;
        [SerializeField] private ImageStore? imageStore;
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

            UpdateBuildingTemplate(territory.BuildingTemplates);
            territory.BuildingTemplates
                .ObserveMove()
                .Subscribe(_ => UpdateBuildingTemplate(territory.BuildingTemplates))
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

        private void UpdateBuildingTemplate(IEnumerable<IBuildable> buildables)
        {
            if (buildingTemplateView == null || imageStore == null) throw new NullReferenceException();

            buildingTemplateView.BuildingTemplateViewDatas = buildables
                .Select(buildable => new BuildingTemplateViewData(buildable.Id,
                    buildable.DisplayName,
                    imageStore.FindBuildingImage(buildable.SystemName)));
        }

        public void OrderBuilding(int townId, Guid buildingId)
        {
            if (_myTerritoryEntity == null) throw new NullReferenceException();
            _myTerritoryEntity.OrderBuilding(townId, buildingId);
        }

        private void CommandFaithToMoney(float faith, float money)
        {
            if (_myTerritoryEntity == null) throw new NullReferenceException();
            
            _myTerritoryEntity.FaithToMoney(faith, money);
        }
    }
}