using System;
using System.Collections.Generic;
using System.Linq;
using Model.Town;
using UniRx;
using UnityEngine;
using View;
using Database;
using Model.Town.Building;

#nullable enable

namespace Presenter
{
    public class TownDetailPresenter : MonoBehaviour
    {
        [SerializeField] private TownsPresenter? townsPresenter;
        [SerializeField] private TownDetailView? townDetailView;
        [SerializeField] private ImageDatabase? imageDatabase;

        private void Start()
        {
            if(townsPresenter == null || townDetailView == null) throw new NullReferenceException();
            
            townDetailView.OnOpened
                .Where(_ => townsPresenter.SelectedTownId.Value > 0)
                .Subscribe(_ => OnOpened())
                .AddTo(this);

            townsPresenter.OnDetailChanged
                .Where(_ => isActiveAndEnabled)
                .Where(townId => townId != 0)
                .Select(townId => townsPresenter.FindEntityById(townId))
                .Subscribe(UpdateDetail)
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
            
            var popData = entity.GetPopData();
            var workplaceData = entity.GetWorkplaces();

            townDetailView.UpdateDivisionContainer(CreateRowData(popData, workplaceData));
        }

        private void UpdateBuildingContainer(TownEntity entity)
        {
            if(townDetailView == null) throw new NullReferenceException();

            var buildingDatas = entity.GetBuildings();
            townDetailView.UpdateBuildingContainer(ConvertBuildingData(buildingDatas));
        }

        private IEnumerable<PopSlotRowViewData> CreateRowData(IEnumerable<PopData> pops, IEnumerable<WorkplaceData> workplaces)
        {
            if(imageDatabase == null) throw new NullReferenceException();
            
            var results = (
                from grouped in workplaces.GroupBy(ws => ws.SlotName) 
                let data = new List<PopSlotViewData>(grouped.Select(wpData =>
                {
                    var worker = pops.FirstOrDefault(pop => pop.WorkplaceGuid == wpData.SlotGuid);

                    var viewData = worker.Id != Guid.Empty
                        ? new PopSlotViewData(wpData.SlotGuid,
                            wpData.SlotName,
                            wpData.SlotTypeName,
                            imageDatabase.FindSlotBackground(wpData.SlotName),
                            worker.Id,
                            worker.Name,
                            worker.TypeName,
                            imageDatabase.FindRaceFaceImage(worker.Name),
                            worker.Consumptions,
                            worker.Produces
                        )
                        : new PopSlotViewData(wpData.SlotGuid, wpData.SlotName, wpData.SlotTypeName,
                            imageDatabase.FindSlotBackground(wpData.SlotName));

                    return viewData;
                })) 
                select new PopSlotRowViewData(grouped.Key, data)).ToList();

            var unemployedSlots = pops
                .Where(pop => pop.WorkplaceGuid.Equals(Guid.Empty))
                .Select(pop => new PopSlotViewData(pop.Id, pop.Name, pop.TypeName, imageDatabase.FindRaceFaceImage(pop.Name),
                    pop.Consumptions, pop.Produces))
                .ToList();
            
            results.Add(new PopSlotRowViewData("無職", unemployedSlots));
            
            return results;
        }

        private IEnumerable<BuildingSlotViewData> ConvertBuildingData(IEnumerable<BuildingData> buildings)
        {
            if (imageDatabase == null) throw new NullReferenceException();

            return buildings
                .Select(b => new BuildingSlotViewData(b.Id, b.Name, 
                    imageDatabase.FindBuildingImage(b.Name)));
        }
    }
}