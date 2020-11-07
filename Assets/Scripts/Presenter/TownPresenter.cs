using System;
using Model.Town;
using Model.Util;
using UnityEngine;
using UnityEngine.EventSystems;
using View;

namespace Presenter
{
    public class TownPresenter : MonoBehaviour
    {
        public TownEntity TownEntity { get; private set; }
        [SerializeField] private string townOutlineName;
        [SerializeField] public int townId;
        private TownOutlinePresenter _outline;

        private void Start()
        {
            _outline = GameObject.Find(townOutlineName).GetComponent<TownOutlinePresenter>();
        }

        public void SetTown(TownEntity townEntity)
        {
            TownEntity = townEntity;
        }

        public void OnClicked(BaseEventData e)
        {
            _outline.UpdateData(GetTownOutlineData());
        }
        
        private TownOutlineData GetTownOutlineData()
        {
            return new TownOutlineData(TownEntity.TownName,
                TownEntity.TownType.GetDescription(),
                TownEntity.GetStoredProducts());
        }
    }
}
