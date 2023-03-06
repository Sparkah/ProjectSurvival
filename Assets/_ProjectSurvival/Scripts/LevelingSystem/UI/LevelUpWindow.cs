using System.Collections.Generic;
using _ProjectSurvival.Scripts.LevelingSystem.Rewards;
using _ProjectSurvival.Scripts.Stats;
using _ProjectSurvival.Scripts.Weapons.ActiveWeapons;
using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.LevelingSystem.UI
{
    public class LevelUpWindow : MonoBehaviour 
    {
        [Inject] private WeaponTypeSelector _attackTypeSelector;
        [Inject] private ActiveWeapons _activeWeapons;
        [Inject] private StatsTypeSelector _statsTypeSelector;
        [Inject] private ActiveStats _activeStats;
        [SerializeField] private Window _window;
        [SerializeField] private PauseControllerSO _pauseControllerSO;
        [Header("Upgrades window")]
        [SerializeField] private GameObject _upgradesPanel;
        [SerializeField] private LevelUpRewardButton[] _levelUpButtons;
        [Header("No more upgrades window")]
        [SerializeField] private GameObject _fullyUpgradedPanel;
        [SerializeField] private LevelUpRewardButton _upgradedCloseButton;
        [Header("Tracking object")]
        [SerializeField] private Behaviour _levelableObject;
        [SerializeField] private bool _includeStatsUpgrades = false;
    
        private ILevelable _levelable => _levelableObject as ILevelable;
        private bool _firstTimeLevelUp = true;

        private void Awake()
        {
            _levelable.OnLevelUp += ShowLevelUpWindow;
            List<IRewardGiver> _rewards = new List<IRewardGiver>();
            if (_includeStatsUpgrades)
            {
                _rewards.Add(_activeStats);
            }

            _rewards.Add(_activeWeapons);
            
            for (int i = 0; i < _levelUpButtons.Length; i++)
            {
                _levelUpButtons[i].Init(this, _rewards);
            }
            _upgradedCloseButton.Init(this);
        }

        private void OnDestroy()
        {
            _levelable.OnLevelUp -= ShowLevelUpWindow;
        }

        public void CloseLevelUpWindow()
        {
            _pauseControllerSO.ResumeGame();
            _window.Close();
        }

        private void ShowLevelUpWindow()
        {
            _pauseControllerSO.PauseGame();
            _upgradesPanel.SetActive(false);
            _fullyUpgradedPanel.SetActive(false);

            WeaponTypeSO[] selectedUpgrades = _attackTypeSelector.SelectWeapons(_levelUpButtons.Length);
        
            if (_includeStatsUpgrades && !_firstTimeLevelUp) 
            {
                StatsTypeSO[] statsUpgrades = _statsTypeSelector.SelectStats(_levelUpButtons.Length);

                ShowAllUpgrades(selectedUpgrades, statsUpgrades);
                _window.Open();
                return;
            }
        
            if (selectedUpgrades.Length != 0)
                ShowUpgrades(selectedUpgrades);
            else
                ShowUpgradedMessage();

            _firstTimeLevelUp = false;
            _window.Open();
        }

        private void ShowUpgrades(WeaponTypeSO[] selectedUpgrades)
        {
            for (int i = 0; i < _levelUpButtons.Length; i++)
            {
                if (i < selectedUpgrades.Length)
                    _levelUpButtons[i].ShowReward(selectedUpgrades[i]);
                else
                    _levelUpButtons[i].Hide();
            }
            _upgradesPanel.SetActive(true);
        }

        private void ShowAllUpgrades(WeaponTypeSO[] selectedUpgrades, StatsTypeSO[] statsSelectedUpgrades)
        {
            for (int i = 0; i < _levelUpButtons.Length; i++)
            {
                var whichUpgrade = Random.Range(0, 2);
                if (whichUpgrade == 0)
                {
                    if (i < selectedUpgrades.Length)
                        _levelUpButtons[i].ShowReward(selectedUpgrades[i]);
                    else
                        _levelUpButtons[i].Hide();
                }
                else
                {
                    if (i < selectedUpgrades.Length)
                    {
                        _levelUpButtons[i].ShowReward(statsSelectedUpgrades[i]);
                    }
                    else
                        _levelUpButtons[i].Hide();
                }
            }
            _upgradesPanel.SetActive(true);
        }

        private void ShowUpgradedMessage()
        {
            _fullyUpgradedPanel.SetActive(true);
        }
    }
}