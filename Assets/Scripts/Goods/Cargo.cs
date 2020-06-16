using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Goods {
    public class Cargo {
        public GoodsEntity Goods { get; private set; }
        public int Amount { get; private set; }

        public Cargo(GoodsEntity _goods, int _amount) {
            Goods = _goods;
            Amount = _amount;
        }
    }
}
