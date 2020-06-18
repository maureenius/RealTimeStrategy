using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Town;
using Assets.Scripts.Util;
using ControllerInfo;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Manager
{
    public class TownController : MonoBehaviour {
        public TownEntity townModel;
        [SerializeField] public int id;

        public void Initialize(TownEntity model)
        {
            townModel = model;
            AddSelectEvents();
        }

        public TownInfo GetTownInfo()
        {
            return new TownInfo(townModel.TownName, townModel.TownType, townModel.GetStoredProducts());
        }

        public List<(string slotTypeName, IEnumerable<PopSlotInfo>)> GetPopSlotInfo()
        {
            return townModel.GetPopSlotInfo();
        }

        public PopInfo GetPopInfo(Guid popId)
        {
            return townModel.GetPopInfo(popId);
        }

        private void AddSelectEvents()
        {
            var trigger = gameObject.AddComponent<EventTrigger>();
            var entry = new EventTrigger.Entry {eventID = EventTriggerType.PointerClick};
            entry.callback.AddListener(e =>
            {
                GameObject.FindGameObjectWithTag("UI")
                    .transform
                    .Find("TownInfo")
                    .GetComponent<TownInfoUi>()
                    .SelectTown(e);
                GameObject.FindGameObjectWithTag("UI")
                    .transform
                    .Find("TownDetail")
                    .GetComponent<TownDetailUi>()
                    .SelectTown(e);
            });
            trigger.triggers.Add(entry);
        }
    }

    public readonly struct TownInfo
    {
        public string TownName { get; }
        public string TownType { get; }
        public List<(string goodsTypeName, int amount)> Storages { get; }

        public TownInfo(string townName, TownType townType, IEnumerable<(string goodsTypeName, int amount)> storages)
        {
            TownName = townName;
            TownType = townType.GetDescription();
            Storages = new List<(string goodsTypeName, int amount)>(storages);
        }
    }
}