using System;
using Model.World;
using UniRx;
using UnityEngine;
using View;

#nullable enable

namespace Presenter
{
    public class TimePresenter : MonoBehaviour
    {
        [SerializeField] private int milliSecForOneTurn = 1000;
        [SerializeField] private HeaderView? headerView;

        private World? _world;
        private bool isPaused = true;

        public void Initialize(World world)
        {
            if (_world != null) throw new InvalidOperationException("Worldは既に初期化されています");
            _world = world;
            
            Observable.Interval(TimeSpan.FromMilliseconds(milliSecForOneTurn))
                .Where(_ => !isPaused)
                .Subscribe(_ => NextTurn())
                .AddTo(this);

            if (headerView == null) throw new NullReferenceException();
            headerView.OnChangePaused
                .Subscribe(timeStop => isPaused = timeStop)
                .AddTo(this);
        }

        private void NextTurn()
        {
            if (headerView == null || _world == null) throw new NullReferenceException();
            
            _world.DoOneTurn();
            headerView.UpdateDate(_world.Date);
        }
    }
}