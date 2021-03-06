﻿using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using Model.Goods;
using Model.Pops;
using Model.Town;
using UniRx;
using UnityEngine;
using View.TownDetail;
using Model.Town.Building;
using Store;

#nullable enable

namespace Presenter
{
    public class TownDetailPresenter : MonoBehaviour
    {
        [SerializeField] private TownsPresenter? townsPresenter;
        [SerializeField] private MyTerritoryPresenter? myTerritoryPresenter;
        [SerializeField] private TownDetailView? townDetailView;
        [SerializeField] private ImageStore? imageDatabase;

        public void Initialize()
        {
            if(townsPresenter == null) throw new NullReferenceException();
            townsPresenter.OnDetailChanged
                .Where(_ => isActiveAndEnabled)
                .Where(townId => townId != 0)
                .Select(townId => townsPresenter.FindEntityById(townId))
                .Subscribe(UpdateDetail)
                .AddTo(this);
            
            if(townDetailView == null) throw new NullReferenceException();
            townDetailView.Initialize();
            townDetailView.OnOpened
                .Where(_ => townsPresenter.SelectedTownId.Value > 0)
                .Subscribe(_ => OnOpened())
                .AddTo(this);

            if (myTerritoryPresenter == null) throw new NullReferenceException();
            townDetailView.OnBuilding
                .Subscribe(item =>
                {
                    var (divisionId, buildingId) = item;
                    myTerritoryPresenter
                        .OrderBuilding(townsPresenter.SelectedTownId.Value, divisionId, buildingId);
                })
                .AddTo(this);
        }

        private void OnOpened()
        {
            if(townsPresenter == null || townDetailView == null) throw new NullReferenceException();
            
            UpdateDetail(townsPresenter.FindEntityById(townsPresenter.SelectedTownId.Value));
            townDetailView.ShowOverPanel();
        }

        private void UpdateDetail(TownEntity entity)
        {
            UpdateDivisionContainer(entity);
            UpdateBuildingContainer(entity);
        }

        private void UpdateDivisionContainer(TownEntity entity)
        {
            if(townDetailView == null) throw new NullReferenceException();
            
            var popData = entity.Pops;
            var workplaceData = entity.GetWorkplaces();

            townDetailView.UpdateDivisionContainer(CreateRowData(popData, workplaceData));
        }

        private void UpdateBuildingContainer(TownEntity entity)
        {
            if(townDetailView == null) throw new NullReferenceException();

            var divisions = entity.Divisions;
            townDetailView.UpdateBuildingContainer(ConvertBuildingData(divisions));
        }

        private IEnumerable<PopSlotRowViewData> CreateRowData(IEnumerable<Pop> pops, IEnumerable<Workplace> workplaces)
        {
            if(imageDatabase == null) throw new NullReferenceException();

            var slots = workplaces.Select(ws =>
            {
                var slot = new PopSlotViewData();
                slot.SetSlot(ws.Id, ws.DisplayName, imageDatabase.FindSlotBackground(ws.SystemName));

                var worker = pops.FirstOrDefault(pop => pop.Workplace == ws);
                if (worker != null)
                {
                    slot.SetWorker(worker.Id, worker.DisplayName, worker.DisplayName,
                        imageDatabase.FindRaceFaceImage(worker.SystemName),
                        worker.ProduceAbilities.Select(pa =>
                            (goods: pa.OutputGoods.DisplayName, amount: pa.ProduceAmount)),
                        worker.ConsumptionTraits()
                            .Select(con => (goods: con.GoodsName, amount: con.Weight)));
                }
                
                return slot;
            });

            var results = slots.GroupBy(slot => slot.SlotName)
                .Select(grouped => new PopSlotRowViewData(grouped.Key, grouped.ToList()));

            var unemployedSlots = pops
                .Where(pop => pop.Workplace == null)
                .Select(pop =>
                {
                    var slot = new PopSlotViewData();
                    slot.SetWorker(pop.Id, pop.DisplayName, pop.DisplayName,
                        imageDatabase.FindRaceFaceImage(pop.SystemName),
                        pop.ProduceAbilities.Select(pa =>
                            (goods: pa.OutputGoods.DisplayName, amount: pa.ProduceAmount)),
                        pop.ConsumptionTraits()
                            .Select(con => (goods: con.GoodsName, amount: con.Weight)));
                    return slot;
                }).ToList();

            return results.Append(new PopSlotRowViewData("無職", unemployedSlots));
        }

        private IEnumerable<BuildingSlotViewData> ConvertBuildingData(IEnumerable<IDivision> divisions)
        {
            if (imageDatabase == null) throw new NullReferenceException();

            return divisions
                .Select(division => division.Building == null ? 
                    new BuildingSlotViewData(division.Id, division.TerrainName, imageDatabase.FindTerrainImage(division.TerrainName)) : 
                    new BuildingSlotViewData(division.Id, division.TerrainName, imageDatabase.FindTerrainImage(division.TerrainName),
                        division.Building.DisplayName, imageDatabase.FindBuildingImage(division.Building.SystemName)));
        }
    }
}