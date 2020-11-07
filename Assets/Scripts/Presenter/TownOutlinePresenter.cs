using System;
using System.Collections.Generic;
using System.Linq;
using Model.Town;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using View;

namespace Presenter
{
    public class TownOutlinePresenter : MonoBehaviour
    {
        private TownOutlineView _view;

        public void UpdateData(TownOutlineData data)
        {
            _view.ShowOverPanel();
            _view.UpdateOutline(data);
        }
        
        private void Start()
        {
            _view = GetComponent<TownOutlineView>();
        }
    }
}
