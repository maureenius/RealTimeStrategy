using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

#nullable enable

namespace View.TownDetail
{
    public class BuildingTemplateTileView : MonoBehaviour
    {
        [SerializeField] private Image? buildingImage;
        private readonly Subject<Guid> _onSelected = new Subject<Guid>();
        public IObservable<Guid> OnSelected => _onSelected;

        private Guid buildingId = Guid.Empty;
        
        public void Initialize(BuildingTemplateViewData newData)
        {
            if (buildingImage == null) throw new NullReferenceException();

            buildingId = newData.Id;
            buildingImage.sprite = newData.BuildingImage;
        }

        public void OnClick()
        {
            _onSelected.OnNext(buildingId);
        }
    }
}