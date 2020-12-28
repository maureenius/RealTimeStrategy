using System;
using System.Collections.Generic;
using UnityEngine;

#nullable enable

namespace Database
{
    [Serializable, CreateAssetMenu(fileName = "WorkplaceData", menuName = "Database/Create Workplace Data")]
    public class WorkplaceData : ScriptableObject
    {
        [SerializeField] private new string name = "";
        public string Name => name;

        [SerializeField] private int id;
        public int Id => id;

        [SerializeField] private string displayName = "";
        public string DisplayName => displayName;

        [SerializeField] private List<MeasuredGoods> products = new List<MeasuredGoods>();
        public List<MeasuredGoods> Products => products;
        
        [SerializeField] private List<MeasuredGoods> consumptions = new List<MeasuredGoods>();
        public List<MeasuredGoods> Consumptions => consumptions;
    }

    [SerializableAttribute]
    public class MeasuredGoods
    {
        [SerializeField] private GoodsData? goods;
        public GoodsData Goods
        {
            get
            {
                if (goods == null) throw new NullReferenceException();
                return goods;
            }
        }

        public float amount;
    }
}