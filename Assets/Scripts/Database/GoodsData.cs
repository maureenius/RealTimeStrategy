using System;
using UnityEngine;

#nullable enable

namespace Database
{
    [Serializable, CreateAssetMenu(fileName = "GoodsData", menuName = "Database/Create Goods Data")]
    public class GoodsData : ScriptableObject
    {
        [SerializeField] private new string name = "";
        public string Name => name;

        [SerializeField] private int id;
        public int Id => id;

        [SerializeField] private string displayName = "";
        public string DisplayName => displayName;
    }
}