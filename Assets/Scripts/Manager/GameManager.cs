using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Assets.Scripts.World;
using System.Linq;
using TMPro;

public class GameManager : MonoBehaviour {
    // [SerializeField] private TextMeshProUGUI textDate;
    //
    // [SerializeField] private int milliSecForOneTurn;
    // [SerializeField] private GameObject townParent;
    // [SerializeField] private GameObject pauseImage;
    // [SerializeField] private GameObject startImage;
    //
    // [SerializeField] private GameObject routeParent;
    // [SerializeField] private GameObject routePrefab;
    //
    // [SerializeField] private int defaultRouteCapacity = 100;
    // [SerializeField] private int defaultRouteLength = 3;
    //
    // private IList<TownController> townControllers;
    // private World world;
    // private bool isPaused;


    // Start is called before the first frame update
    // void Start() {
    //     var townControllers = townParent.GetComponentsInChildren<TownController>().ToList();
    //
    //     world = new World();
    //
    //     // ひとまず全都市を相互に結線する
    //     //townControllers.ForEach(tStart => {
    //     //    townControllers.Where(tEnd => tStart != tEnd)
    //     //        .ToList()
    //     //        .ForEach(tEnd => {
    //     //            Connect(tStart, tEnd);
    //     //            Connect(tEnd, tStart);
    //     //        });
    //     //});
    //
    //     Observable.Interval(TimeSpan.FromMilliseconds(milliSecForOneTurn))
    //         .Where(_ => !isPaused)
    //         .Subscribe(_ => NextTurn())
    //         .AddTo(this);
    // }
    //
    // void NextTurn() {
    //     world.DoOneTurn();
    //     UpdateDateText(world.Date);
    // }
    //
    // void UpdateDateText(DateTime _date) {
    //     textDate.text = _date.ToString("yyyy/MM/dd");
    // }
    //
    // public void TogglePause() {
    //     isPaused = !isPaused;
    //     pauseImage.SetActive(!isPaused);
    //     startImage.SetActive(isPaused);
    // }

    //public void Connect(TownController startTC, TownController endTC) {
    //    world.RouteLayer.Connect(startTC.townModel, endTC.townModel, defaultRouteCapacity, defaultRouteLength);

    //    var routeObject = (GameObject)Instantiate(routePrefab);
    //    routeObject.transform.parent = routeParent.transform;
    //    var routeController = routeObject.GetComponent<RouteController>();
    //    routeController.Initialize();
    //    routeObject.GetComponent<RouteController>().Render(startTC.transform.position,
    //        endTC.transform.position);
    //}
}
