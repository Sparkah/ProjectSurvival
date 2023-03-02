using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelUpRewardButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Text _attackLabel;
    private LevelUpWeaponsWindow _levelUpWindow;
    private IRewardGiver _rewardGiver;
    private IReward _reward;

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnCick);
    }

    public void Init(LevelUpWeaponsWindow levelUpWindow, IRewardGiver rewardGiver = null)
    {
        _levelUpWindow = levelUpWindow;
        _rewardGiver = rewardGiver;
        _button.onClick.AddListener(OnCick);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ShowReward(IReward reward)
    {
        _reward = reward;
        _attackLabel.text = _reward.Title;
        gameObject.SetActive(true);
    }

    private void OnCick()
    {
        if (_reward != null && _rewardGiver != null)
            _rewardGiver.GiveReward(_reward);
        _levelUpWindow.CloseLevelUpWindow();
    }
}
