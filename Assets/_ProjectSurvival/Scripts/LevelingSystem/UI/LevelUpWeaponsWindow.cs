using UnityEngine;
using Zenject;

public class LevelUpWeaponsWindow : MonoBehaviour
{
    [Inject] private WeaponTypeSelector _attackTypeSelector;
    [Inject] private ActiveWeapons _activeWeapons;
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
    private ILevelable _levelable => _levelableObject as ILevelable;

    private void Awake()
    {
        _levelable.OnLevelUp += ShowLevelUpWindow;
        for (int i = 0; i < _levelUpButtons.Length; i++)
        {
            _levelUpButtons[i].Init(this, _activeWeapons);
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
        if (selectedUpgrades.Length != 0)
            ShowUpgrades(selectedUpgrades);
        else
            ShowUpgradedMessage();

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

    private void ShowUpgradedMessage()
    {
        _fullyUpgradedPanel.SetActive(true);
    }
}
