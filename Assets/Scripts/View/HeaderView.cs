﻿using System;
using TMPro;
using UniRx;
using UnityEngine;

#nullable enable

namespace View
{
    public class HeaderView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI? dateText;
        [SerializeField] private TextMeshProUGUI? faithPointText;
        [SerializeField] private TextMeshProUGUI? moneyText;
        [SerializeField] private GameObject? pauseButton;
        [SerializeField] private GameObject? playButton;

        private Subject<bool> _changePausedSubject = new Subject<bool>();
        public IObservable<bool> OnChangePaused => _changePausedSubject;

        public void UpdateDate(DateTime dateTime)
        {
            if (dateText == null) throw new NullReferenceException();
            
            dateText.text = dateTime.ToString("yyyy/MM/dd");
        }

        public void PlayGame()
        {
            if (pauseButton == null || playButton == null) throw new NullReferenceException();
            
            _changePausedSubject.OnNext(false);
            pauseButton.SetActive(true);
            playButton.SetActive(false);
        }
        
        public void PauseGame()
        {
            if (pauseButton == null || playButton == null) throw new NullReferenceException();
            
            _changePausedSubject.OnNext(true);
            pauseButton.SetActive(false);
            playButton.SetActive(true);
        }

        public void UpdateFaithPoint(float point)
        {
            if (faithPointText == null) throw new NullReferenceException();

            faithPointText.text = point.ToString("F1");
        }

        public void UpdateMoney(float money)
        {
            if (moneyText == null) throw new NullReferenceException();

            moneyText.text = money.ToString("F1");
        }
    }
}