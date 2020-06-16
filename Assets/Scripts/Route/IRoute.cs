using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Goods;
using static Assets.Scripts.Util.Util;

namespace Assets.Scripts.Route {
    public interface IRoute {
        int Capacity { get; }
        void DoOneTurn();
        StatusCode PushCargo(Cargo cargo);
        Cargo TakeCargo();
    }
}
