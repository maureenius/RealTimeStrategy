// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Manager;
// using UniRx;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using TMPro;
//
// namespace UI
// {
//     public class TownInfoUi : MonoBehaviour
//     {
//         [SerializeField] private int milliSecForUpdate = 100;
//         
//         private TownController selectedTown;
//         private TextMeshProUGUI townNameText;
//         private TextMeshProUGUI townTypeText;
//         private List<TextMeshProUGUI> townStorageTexts;
//             
//         private void Start()
//         {
//             townNameText = transform.Find("Name").GetComponent<TextMeshProUGUI>();
//             townTypeText = transform.Find("Attribute").GetComponent<TextMeshProUGUI>();
//             townStorageTexts = transform.Find("Storages").GetComponentsInChildren<TextMeshProUGUI>().ToList();
//                 
//             Observable.Interval(TimeSpan.FromMilliseconds(milliSecForUpdate))
//                 .Where(_ => selectedTown != null)
//                 .Subscribe(_ => UpdateTownInfo())
//                 .AddTo(this);
//         }
//
//         public void ShowOverPanel() {
//             gameObject.SetActive(true);
//         }
//
//         public void HideOverPanel() {
//             gameObject.SetActive(false);
//         }
//
//         public void SelectTown(BaseEventData e) {
//             selectedTown = ((PointerEventData)e).pointerEnter.GetComponent<TownController>();
//
//             gameObject.SetActive(true);
//         }
//
//         public void DeselectTown(BaseEventData e) {
//             selectedTown = null;
//
//             gameObject.SetActive(false);
//         }
//
//         private void UpdateTownInfo()
//         {
//             var info = selectedTown.GetTownInfo();
//             townNameText.text = info.TownName;
//             townTypeText.text = info.TownType;
//             townStorageTexts.ForEach(text =>
//             {
//                 var (goodsTypeName, amount) = info.Storages.FirstOrDefault();
//                 if (goodsTypeName == null) return;
//                 
//                 text.text = goodsTypeName + " : " + amount;
//                 info.Storages.RemoveAt(0);
//             });
//         }
//     }
// }
