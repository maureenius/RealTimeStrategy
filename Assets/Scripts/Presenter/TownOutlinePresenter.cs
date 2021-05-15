using System;
using Model.Town;
using Model.Util;
using UniRx;
using UnityEngine;
using View;

#nullable enable

namespace Presenter
{
    public class TownOutlinePresenter : MonoBehaviour
    {
        [SerializeField] private TownsPresenter? townsPresenter;
        
        [SerializeField] private TownOutlineView? view;

        private void UpdateData(TownEntity entity)
        {
            if (view == null) throw new NullReferenceException();
            
            view.ShowOverPanel();
            view.UpdateOutline(GetTownOutlineData(entity));
        }

        public void Initialize()
        {
            if (townsPresenter == null) throw new NullReferenceException();

            townsPresenter.SelectedTownId
                .Where(townId => townId != 0)
                .Select(townId => townsPresenter.FindEntityById(townId))
                .Subscribe(UpdateData)
                .AddTo(this);

            townsPresenter.OnOutlineChanged
                .Where(townId => townId != 0)
                .Select(townId => townsPresenter.FindEntityById(townId))
                .Subscribe(UpdateData)
                .AddTo(this);
        }
        
        private static TownOutlineData GetTownOutlineData(TownEntity entity) =>
            new TownOutlineData(entity.TownName,
                entity.TownType.GetDescription(),
                entity.Storages);
    }
}
