using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable enable

namespace Database
{
    public static class GoodsDatabase
    {
        private static readonly Dictionary<string, GoodsData> GoodsDataList =
            Resources.LoadAll<GoodsData>("Database/GoodsData/")
                .ToDictionary(data => data.Name);

        public static GoodsData Find(string name)
        {
            return GoodsDataList[name];
        }

        public static GoodsData Find(GoodsName name)
        {
            return GoodsDataList[name.ToString()];
        }

        public static IReadOnlyDictionary<string, GoodsData> All()
        {
            return GoodsDataList;
        }
    }

    public enum GoodsName
    {
        Flour,
        Sugar,
        Cookie
    }
}