using System;
using System.Linq;
using UnityEngine;
using Assets.Scripts.World;
using TMPro;
using UniRx;

namespace Manager
{
    public class WorldManager : MonoBehaviour
    {
        [SerializeField] private int milliSecForOneTurn = 1000;

        [SerializeField] private GameObject townParent;
        [SerializeField] private TextMeshProUGUI dateText;
        [SerializeField] private GameObject pauseImage;
        [SerializeField] private GameObject playImage;

        public bool isPaused = true;
        
        private World world;

        public void PlayGame()
        {
            isPaused = false;
            pauseImage.SetActive(true);
            playImage.SetActive(false);
        }

        public void PauseGame()
        {
            isPaused = true;
            pauseImage.SetActive(false);
            playImage.SetActive(true);
        }

        private void Start()
        {
            world = new World();
            InitializeTowns();

            Observable.Interval(TimeSpan.FromMilliseconds(milliSecForOneTurn))
                .Where(_ => !isPaused)
                .Subscribe(_ => NextTurn())
                .AddTo(this);
        }

        private void NextTurn()
        {
            world.DoOneTurn();
            dateText.text = world.Date.ToString("yyyy/MM/dd");
        }

        private void InitializeTowns()
        {
            var townObjects = townParent.GetComponentsInChildren<TownController>();

            world.Towns.ForEach(townModel =>
            {
                townObjects.FirstOrDefault(t => t.id == townModel.Id)?.Initialize(townModel);
            });
        }
    }
}
