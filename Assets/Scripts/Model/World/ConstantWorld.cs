using System;
using System.Collections.Generic;
using System.Linq;
using Model.Commerce;
using Model.Goods;
using Model.Race;
using Model.Region;
using Model.Route;
using Model.Territory;
using Model.Town;

#nullable enable

namespace Model.World
{
    public class ConstantWorld : World
    {
        public ConstantWorld()
        {
            Commerces = InitialCommerce(Towns);
        }

        private IEnumerable<CommerceEntity> InitialCommerce(IEnumerable<TownEntity> towns)
        {
            return towns.Select(town => new CommerceEntity(town, Territories));
        }
    }
}