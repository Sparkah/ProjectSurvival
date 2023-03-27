using System;
using _ProjectSurvival.Infrastructure;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.UpgradeTree
{
    public class UpgradeTree : MonoBehaviour
    {
        [Inject] private World _world;
        
        public UpgradePopup UpgradePopup;
        public CostProgressionSO CostProgression;
        
        [SerializeField] private TextMeshProUGUI _moneyAvailable;
        [SerializeField] private TextMeshProUGUI _moneyRequiredForNextPurchase;
        
        private UpgradesRowData[] _upgradesRowDatas;
        private CompositeDisposable _disposable = new CompositeDisposable();
        private ReactiveProperty<int> _purchasedUpgrades = new ReactiveProperty<int>();
        
        private void Start()
        {
            UpgradePopup.Construct(this);
            _upgradesRowDatas = GetComponentsInChildren<UpgradesRowData>();
            _world.UpgradeLevels.ObserveReplace().Subscribe(x => _purchasedUpgrades.Value+=1).AddTo(_disposable);
            _purchasedUpgrades.Subscribe(_ => UpdateUpgradesCount()).AddTo(_disposable);
            SetUpUpgradeRows();
            UpdateUpgradesCount();
        }

        public void UpdateUpgradesCount()
        {
            _moneyAvailable.text = MathF.Round(_world.Gold.Value,1).ToString();
            _moneyRequiredForNextPurchase.text =  "Стоимость: " + CostProgression.CostProgression[_world.CurrentUpgradeID.Value].ToString();
        }

        public void SetUpUpgradeRows()
        {
            foreach (var upgradesRowData in _upgradesRowDatas)
            {
                upgradesRowData.SetUp();
            }
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}