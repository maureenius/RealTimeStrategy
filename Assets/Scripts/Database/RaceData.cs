using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

#nullable enable

namespace Database
{
    [Serializable, CreateAssetMenu(fileName = "RaceData", menuName = "Database/Create Race Data")]
    public class RaceData : ScriptableObject
    {
        [SerializeField] private new string name = "";
        public string Name => name;

        [SerializeField] private int id;
        public int Id => id;

        [SerializeField] private string displayName = "";
        public string DisplayName => displayName;
        
        [SerializeField] private List<MeasuredGoods> consumptions = new List<MeasuredGoods>();
        public List<MeasuredGoods> Consumptions => consumptions;
    }
}