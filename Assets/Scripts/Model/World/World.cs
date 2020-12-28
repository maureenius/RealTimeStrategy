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
using Model.Town.Building;

#nullable enable

namespace Model.World {
    public class World
    {
        public IEnumerable<TownEntity> Towns { get; private set; } = new List<TownEntity>();

        protected IEnumerable<CommerceEntity> Commerces = new List<CommerceEntity>();
        public IEnumerable<TerritoryEntity> Territories { get; private set; } = new List<TerritoryEntity>();
        protected IEnumerable<RegionEntity> Regions = new List<RegionEntity>();

        public DateTime Date = new DateTime(1000, 1, 1);

        public void DoOneTurn() {
            Date = Date.AddDays(1);

            foreach (var town in Towns)
            {
                town.DoOneTurn();
            }

            foreach (var territory in Territories)
            {
                territory.DoOneTurn();
            }

            foreach (var commerce in Commerces)
            {
                commerce.DoOneTurn();
            }
            
            RouteLayer.GetInstance().DoOneTurn();
        }

        public void InitializeTowns(IEnumerable<TownEntity> towns)
        {
            if (Towns.Any()) throw new InvalidOperationException("Townsは既に初期化されています");

            Towns = towns;
        }
        
        public void InitializeTerritories(IEnumerable<TerritoryEntity> territories)
        {
            if (Territories.Any()) throw new InvalidOperationException("Territoriesは既に初期化されています");

            Territories = territories;
        }

        public void InitializeCommerce(IEnumerable<CommerceEntity> commerces)
        {
            if (Commerces.Any()) throw new InvalidOperationException("Commercesは既に初期化されています");

            Commerces = commerces;
        }
    }
}
