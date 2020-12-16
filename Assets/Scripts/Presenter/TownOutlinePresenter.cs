using Model.Town;
using Model.Util;
using UniRx;
using UnityEngine;
using View;

namespace Presenter
{
    public class TownOutlinePresenter : MonoBehaviour
    {
        [SerializeField] private TownsPresenter townsPresenter;
        
        private TownOutlineView _view;

        private void UpdateData(TownEntity entity)
        {
            _view.ShowOverPanel();
            _view.UpdateOutline(GetTownOutlineData(entity));
        }
        
        private void Start()
        {
            _view = GetComponent<TownOutlineView>();

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
                entity.GetStoredProducts());
    }
}
