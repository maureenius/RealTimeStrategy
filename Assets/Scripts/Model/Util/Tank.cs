using System;
using UniRx;

# nullable enable

namespace Model.Util
{
    // float量を持ち、消費と備蓄が可能なものを扱うクラス
    public class Tank
    {
        private readonly float limit;
        public float Volume { get; private set; }
        private readonly Subject<float>? changeSubject;
        
        public Tank(float limit, float volume=0f, Subject<float>? subject=null)
        {
            this.limit = limit;
            Volume = volume;
            changeSubject = subject;
        }

        public void Store(float input)
        {
            Volume = Math.Min(limit, Volume + input);
            
            changeSubject?.OnNext(Volume);
        }

        public bool CanConsume(float output)
        {
            return Volume >= output;
        }

        public void Consume(float output)
        {
            if (!CanConsume(output)) throw new InvalidOperationException();

            Volume -= output;
            changeSubject?.OnNext(Volume);
        }
    }
}