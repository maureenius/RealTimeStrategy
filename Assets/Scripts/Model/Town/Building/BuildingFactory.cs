using System.Collections.Generic;
using Database;
using Model.Goods;
using Model.Race;

#nullable enable

namespace Model.Town.Building
{
    public static class BuildingFactory
    {
        public static SimpleProducer FlourFarm()
        {
            return new SimpleProducer(BuildingDatabase.Find(BuildingName.WheatField));
        }
        
        public static SimpleProducer SugarCaneField()
        {
            return new SimpleProducer(BuildingDatabase.Find(BuildingName.SugarCaneField));
        }
        
        public static SimpleProducer Confectionery()
        {
            return new SimpleProducer(BuildingDatabase.Find(BuildingName.Confectionery));
        }
    }
}