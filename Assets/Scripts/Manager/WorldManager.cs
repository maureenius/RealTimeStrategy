using System;
using JetBrains.Annotations;
using Model.Route;
using Model.World;
using Presenter;
using UnityEngine;
using TMPro;
using UniRx;

#nullable enable

namespace Manager
{
    public class WorldManager : MonoBehaviour
    {
        [SerializeField] private int milliSecForOneTurn = 1000;
        
        [SerializeField] private TextMeshProUGUI? dateText;
        [SerializeField] private GameObject? pauseImage;
        [SerializeField] private GameObject? playImage;

        public bool isPaused = true;
        
        private readonly World _world = new World();

        public void PlayGame()
        {
            if (pauseImage == null || playImage == null) throw new NullReferenceException();
            
            isPaused = false;
            pauseImage.SetActive(true);
            playImage.SetActive(false);
        }

        public void PauseGame()
        {
            if (pauseImage == null || playImage == null) throw new NullReferenceException();
            
            isPaused = true;
            pauseImage.SetActive(false);
            playImage.SetActive(true);
        }

        private void Start()
        {
            InitializeTowns();
            InitializeRoutes();

            Observable.Interval(TimeSpan.FromMilliseconds(milliSecForOneTurn))
                .Where(_ => !isPaused)
                .Subscribe(_ => NextTurn())
                .AddTo(this);
        }

        private void NextTurn()
        {
            if (dateText == null) throw new NullReferenceException();
            
            _world.DoOneTurn();
            dateText.text = _world.Date.ToString("yyyy/MM/dd");
        }

        private void InitializeTowns()
        {
            GetComponent<TownsPresenter>().Initialize(_world.Towns);
        }

        private void InitializeRoutes()
        {
            GetComponent<RoutesPresenter>().Initialize(RouteLayer.GetInstance().Routes);
        }
    }
}
