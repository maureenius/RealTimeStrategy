using System;
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
        
        private readonly World world = new World();

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

            Observable.Interval(TimeSpan.FromMilliseconds(milliSecForOneTurn))
                .Where(_ => !isPaused)
                .Subscribe(_ => NextTurn())
                .AddTo(this);
        }

        private void NextTurn()
        {
            if (dateText == null) throw new NullReferenceException();
            
            world.DoOneTurn();
            dateText.text = world.Date.ToString("yyyy/MM/dd");
        }

        private void InitializeTowns()
        {
            GetComponent<TownsPresenter>().Initialize(world.Towns);
        }
    }
}
