using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Goods;

namespace Assets.Scripts.Town {
    public class Storage {
        public Goods.GoodsEntity Goods { get; private set; }
        public int Amount { get; private set; }
        public int AmountLimit { get; private set; }

        public Storage(Goods.GoodsEntity _goods, int _amountLimit) {
            Goods = _goods;
            AmountLimit = _amountLimit;
        }

        public Util.Util.StatusCode Consume(int amount) {
            if (!CanConsume(amount)) return Util.Util.StatusCode.FAIL;

            Amount -= amount;
            return Util.Util.StatusCode.SUCCESS;
        }

        public void Store(int amount) {
            Amount += amount;
            if (Amount > AmountLimit) Amount = AmountLimit;
        }

        private bool CanConsume(int amount) {
            return Amount >= amount;
        }
    }
}
