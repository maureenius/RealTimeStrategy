﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ControllerInfo
{
    public readonly struct PopInfo
    {
        public Guid Id { get; }
        public string Name { get; }
        public List<(string goodsName, double amount)> Consumptions { get; }
        public List<(string goodsName, double amount)> Produces { get; }
        public Guid WorkplaceGuid { get; }
        public string WorkplaceName { get; }

        public PopInfo(Guid id, string name, 
            IEnumerable<(string goodsName, double amount)> consumptions, 
            IEnumerable<(string goodsName, double amount)> produces,
            Guid workplaceGuid, string workplaceName)
        {
            Id = id;
            Name = name;
            Consumptions = consumptions.ToList();
            Produces = produces.ToList();
            WorkplaceGuid = workplaceGuid;
            WorkplaceName = workplaceName;
        }
    }
}