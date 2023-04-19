using System;
using System.Collections.Generic;
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
            //_world.Gold.Value = 1100; // => get cheat gold
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

        public void ResetUpgradeTree()
        {
            _purchasedUpgrades.Value = 0;

            List<UpgradeTypes> keys = new List<UpgradeTypes>(_world.UpgradeLevels.Keys);
            foreach (UpgradeTypes key in keys)
            {
                _world.UpgradeLevels[key] = 0;
            }

            for (int i = 0; i < _world.CurrentUpgradeID.Value; i++)
            {
                Debug.Log("adding gold for upgrade");
                _world.Gold.Value += CostProgression.CostProgression[i];
            }
            _world.CurrentUpgradeID.Value = 0;
            Debug.Log("resetting upgrades");
            Start();
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}