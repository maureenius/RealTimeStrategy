using System;
using System.Collections.Generic;
using System.Linq;
using Model.Town;
using Model.Town.Building;
using Model.Util;
using UniRx;

#nullable enable

namespace Model.Territory {
    public abstract class TerritoryEntity {
        protected readonly IList<TownEntity> Towns = new List<TownEntity>();
        protected readonly IList<IBuildable> BuildingTemplates = new List<IBuildable>();
        public string Name { get; }
        public bool IsPlayer { get; }
        public Tank Money { get; }
        public Tank FaithPoint { get; }

        private readonly Subject<Unit> _turnPassedSubject = new Subject<Unit>();
        public IObservable<Unit> OnTurnPassed => _turnPassedSubject;

        private readonly Subject<Unit> _monthPassedSubject = new Subject<Unit>();
        public IObservable<Unit> OnMonthPassed => _monthPassedSubject;

        private readonly Subject<float> _faithPointSubject = new Subject<float>();
        public IObservable<float> OnChangeFaithPoint => _faithPointSubject;

        private readonly Subject<float> _moneySubject = new Subject<float>();
        public IObservable<float> OnChangeMoney => _moneySubject;

        protected TerritoryEntity(string name, float money, bool isPlayer=false) {
            Name = name;
            Money = new Tank(100000, money, _moneySubject);
            FaithPoint = new Tank(10000, 0, _faithPointSubject);
            IsPlayer = isPlayer;
        }

        public void AttachTowns(TownEntity attachedTown)
        {
            Towns.Add(attachedTown);
        }

        public abstract void InitializeTowns();

        public void AddBuildingTemplate(IBuildable template) {
            if (BuildingTemplates.FirstOrDefault(t => t.SystemName == template.SystemName) == null)
            {
                BuildingTemplates.Add(template.Clone());
            }
        }

        public void DoOneTurn()
        {
            _turnPassedSubject.OnNext(Unit.Default);
        }

        public void DoAtMonthBeginning()
        {
            CollectFaith();
            _monthPassedSubject.OnNext(Unit.Default);
        }

        public void FaithToMoney(float faith, float money)
        {
            if (!FaithPoint.CanConsume(faith)) return;
            
            FaithPoint.Consume(faith);
            Money.Store(money);
        }
        
        private void CollectFaith()
        {
            FaithPoint.Store(Towns.Sum(town => town.CollectFaith()));
        }

        public bool IsOwn(TownEntity town)
        {
            return Towns.Contains(town);
        }
    }

    class Territory : TerritoryEntity {
        public Territory(string name, float money, bool isPlayer=false) : base(name, money, isPlayer) {
            InitializeBuildingTemplate();
        }

        private void InitializeBuildingTemplate()
        {
            AddBuildingTemplate(BuildingFactory.FlourFarm());
            AddBuildingTemplate(BuildingFactory.SugarCaneField());
            AddBuildingTemplate(BuildingFactory.Confectionery());
        }
        
        public override void InitializeTowns()
        {
            Towns.ToList().ForEach(town =>
            {
                town.Build(BuildingTemplates.First(template => template.SystemName == "小麦農場"));
                town.Build(BuildingTemplates.First(template => template.SystemName == "さとうきび畑"));
                town.Build(BuildingTemplates.First(template => template.SystemName == "菓子工房"));
            });
        }
    }

    public static class TerritoryFactory {
        public static TerritoryEntity Create(string name, float money) {
            return new Territory(name, money);
        }

        public static TerritoryEntity CreatePlayer(string name, float money)
        {
            return new Territory(name, money, isPlayer: true);
        }
    }

    public class TerritoryRate
    {
        public readonly TerritoryEntity Territory;
        public double Rate;

        public TerritoryRate(TerritoryEntity territory, double rate)
        {
            Territory = territory;
            Rate = rate;
        }
    }
}
