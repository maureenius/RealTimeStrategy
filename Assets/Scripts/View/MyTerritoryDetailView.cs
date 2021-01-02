using System;
using UniRx;
using UnityEngine;

#nullable enable

namespace View
{
    public class MyTerritoryDetailView : MonoBehaviour
    {
        private readonly Subject<(float faith, float money)>
            _toMoneySubject = new Subject<(float faith, float money)>();

        public IObservable<(float faith, float money)> OnCommandToMoney => _toMoneySubject;
        
        public void ToMoney()
        {
            const float faithAmount = 10f;
            const float moneyAmount = 1000f;
            
            _toMoneySubject.OnNext((faith: faithAmount, money: moneyAmount));
        }
    }
}