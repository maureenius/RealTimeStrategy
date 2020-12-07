using System;
using System.Collections.Generic;
using System.Linq;
using Model.Town;
using UniRx;
using UnityEngine;
using View;
using Database;

namespace Presenter
{
    public class TownDetailPresenter : MonoBehaviour
    {
        [SerializeField] private TownsPresenter townsPresenter;
        [SerializeField] private TownDetailView townDetailView;
        [SerializeField] private ImageDatabase imageDatabase;

        private void Start()
        {
            townDetailView.OnOpened
                .Where(_ => townsPresenter.SelectedTownId.Value > 0)
                .Subscribe(_ => OnOpened())
                .AddTo(this);

            townsPresenter.OnDetailChanged
                .Where(townId => townId != 0)
                .Select(townId => townsPresenter.FindEntityById(townId))
                .Subscribe(UpdateDetail)
                .AddTo(this);
        }

        private void OnOpened()
        {
            UpdateDetail(townsPresenter.FindEntityById(townsPresenter.SelectedTownId.Value));
            townDetailView.ShowOverPanel();
        }

        private void UpdateDetail(TownEntity entity)
        {
            UpdatePopContainer(entity);
            UpdateDivisionContainer(entity);
        }

        private void UpdatePopContainer(TownEntity entity)
        {
            
        }

        private void UpdateDivisionContainer(TownEntity entity)
        {
            var popData = entity.GetPopData();
            var workplaceData = entity.GetWorkplaces();

            townDetailView.UpdateDivisionContainer(CreateRowData(popData, workplaceData));
        }

        private IEnumerable<PopSlotRowViewData> CreateRowData(IEnumerable<PopData> pops, IEnumerable<WorkplaceData> workplaces)
        {
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
                            imageDatabase.FindRaceFaceImage(worker.Name)
                        )
                        : new PopSlotViewData(wpData.SlotGuid, wpData.SlotName, wpData.SlotTypeName,
                            imageDatabase.FindSlotBackground(wpData.SlotName));

                    return viewData;
                })) 
                select new PopSlotRowViewData(grouped.Key, data)).ToList();

            var unemployedSlots = pops
                .Where(pop => pop.WorkplaceGuid.Equals(Guid.Empty))
                .Select(pop => new PopSlotViewData(pop.Id, pop.Name, pop.TypeName, imageDatabase.FindRaceFaceImage(pop.Name)))
                .ToList();
            
            results.Add(new PopSlotRowViewData("無職", unemployedSlots));
            
            return results;
        }
    }
}