﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Goods;
using Assets.Scripts.Town.Terrain;
using static Assets.Scripts.Util.Util;

namespace Assets.Scripts.Town.Building {
    public interface INamed {
        Guid Id { get; }
        string Name { get; }
        string TypeName { get; }
    }

    public interface IBuildable : INamed, ICloneable
    {
        // IEnumerable<TerrainEntity> buildableTerrain;
    }

    public interface IWorkable : INamed {
        int WorkerLimit { get; }
        bool CanWork(Pop pop);
    }

    public interface IHasProduceAbility
    {
        IList<ProduceAbility> ProduceAbilities { get; }
    }
}