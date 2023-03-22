using _ProjectSurvival.Scripts.LevelingSystem.Rewards;
using _ProjectSurvival.Scripts.Stats;
using _ProjectSurvival.Scripts.Weapons.ActiveWeapons;
using _ProjectSurvival.Scripts.Weapons.WeaponTypes;
using System.Collections.Generic;
using _ProjectSurvival.Scripts.Audio;
using UnityEngine;
using Zenject;
using System.Linq;

namespace _ProjectSurvival.Scripts.LevelingSystem.UI
{
    public class LevelUpWindow : MonoBehaviour
    {
        [Inject] private WeaponTypeSelector _attackTypeSelector;
        [Inject] private ActiveWeapons _activeWeapons;
        [Inject] private StatsTypeSelector _statsTypeSelector;
        [Inject] private ActiveStats _activeStats;
        [Inject] private AudioSystem _audioSystem;
        
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
            AudioPlayer.Audio.PlayOneShotSound(AudioSounds.LevelUp);
            _audioSystem.StopSceneMusic(true);

            WeaponTypeSO[] selectedWeapons = _attackTypeSelector.SelectWeapons(_levelUpButtons.Length);
            StatsTypeSO[] selectedStats = null;

            if (_firstTimeLevelUp)
            {
                ShowUpgrades(selectedWeapons, null);
                _firstTimeLevelUp = false;
            }
            else
            {
                if (_includeStatsUpgrades)
                    selectedStats = _statsTypeSelector.SelectStats(_levelUpButtons.Length);

                if (HasNoUpgrades(selectedWeapons, selectedStats))
                    ShowUpgradedMessage();
                else
                    ShowUpgrades(selectedWeapons, selectedStats);
            }
            _window.Open();
        }

        private bool HasNoUpgrades(WeaponTypeSO[] selectedWeaponsUpgrades, StatsTypeSO[] selectedStatsUpgrades)
        {
            return IsEmpty(selectedWeaponsUpgrades) && IsEmpty(selectedStatsUpgrades);
        }

        private bool IsEmpty(Object[] array)
        {
            return array == null || array.Length == 0;
        }

        private void ShowUpgradedMessage()
        {
            _fullyUpgradedPanel.SetActive(true);
        }

        private void ShowUpgrades(WeaponTypeSO[] selectedWeaponsUpgrades, StatsTypeSO[] selectedStatsUpgrades)
        {
            List<int> possibleWeapons = CreateIndexesList(selectedWeaponsUpgrades);
            List<int> possibleStats = CreateIndexesList(selectedStatsUpgrades);

            RewardType whichUpgrade;
            int upgradeIndex;
            for (int i = 0; i < _levelUpButtons.Length; i++)
            {
                if (!HasNotListedUpgrades(possibleWeapons, possibleStats))
                {
                    _levelUpButtons[i].Hide();
                }
                else
                {
                    whichUpgrade = SelectUpgradeType(possibleWeapons, possibleStats);
                    switch (whichUpgrade)
                    {
                        case RewardType.Weapon:
                            upgradeIndex = SelectIndex(possibleWeapons);
                            ShowWeaponUpgrade(i, selectedWeaponsUpgrades[upgradeIndex]);
                            possibleWeapons.Remove(upgradeIndex);
                            break;
                        case RewardType.Stat:
                            upgradeIndex = SelectIndex(possibleStats);
                            ShowStatUpgrade(i, selectedStatsUpgrades[upgradeIndex]);
                            possibleStats.Remove(upgradeIndex);
                            break;
                    }
                }
                _upgradesPanel.SetActive(true);
            }
        }

        private List<int> CreateIndexesList(Object[] array)
        {
            if (IsEmpty(array))
                return new List<int>();
            else
                return new List<int>(Enumerable.Range(0, array.Length));
        }

        private bool HasNotListedUpgrades(List<int> possibleWeapons, List<int> possibleStats)
        {
            return possibleWeapons.Count > 0 || possibleStats.Count > 0;
        }

        private RewardType SelectUpgradeType(List<int> possibleWeapons, List<int> possibleStats) //Bad scalability
        {
            if (possibleWeapons.Count > 0 && possibleStats.Count > 0)
                return (RewardType)Random.Range(0, 2);
            else if (possibleWeapons.Count > 0)
                return RewardType.Weapon;
            else
                return RewardType.Stat;
        }

        private int SelectIndex(List<int> indexesList)
        {
            return indexesList[Random.Range(0, indexesList.Count)];
        }

        private void ShowWeaponUpgrade(int buttonIndex, WeaponTypeSO selectedWeapon)
        {
            int level = _activeWeapons.GetRewardLevel(selectedWeapon);
            bool isNew = level == 0;
            if (isNew)
            {
                //level = TODO: load from tree init level
            }
            _levelUpButtons[buttonIndex].ShowReward(selectedWeapon, level, isNew);
        }

        private void ShowStatUpgrade(int buttonIndex, StatsTypeSO selectedStat)
        {
            int level = _activeStats.GetRewardLevel(selectedStat);
            _levelUpButtons[buttonIndex].ShowReward(selectedStat, level, false);
        }
    }
}