using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Model.Goods;

# nullable enable

namespace Model.Town
{
    public class TownStorage
    {
        private readonly List<Storage> storages;
        public List<Storage> Storages => storages;

        public TownStorage()
        {
            storages = new List<Storage>();
        }

        public void Store(IEnumerable<Cargo> cargoes)
        {
            foreach (var cargo in cargoes)
            {
                var storage = GetStorage(cargo.Goods) ?? CreateStorage(cargo.Goods, 1000);

                storage.Store(cargo.Amount.Volume);
            }
        }
        
        public IEnumerable<Cargo> PickUpAll()
        {
            return storages.Select(s => s.PickUpAll());
        }
        
        private Storage? GetStorage(GoodsEntity target) {
            return storages.FirstOrDefault(storage => storage.Goods == target);
        }
        
        private Storage CreateStorage(GoodsEntity goods, int limit)
        {
            var storage = new Storage(goods, limit);
            storages.Add(storage);

            return storage;
        }
    }
}