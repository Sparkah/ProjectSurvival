using System.Collections.Generic;
using _ProjectSurvival.Scripts.LevelingSystem.Rewards;
using _ProjectSurvival.Scripts.LevelingSystem.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelUpRewardButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Text _attackLabel;
    private LevelUpWindow _levelUpWindow;
    private List <IRewardGiver> _rewardGiver = new List<IRewardGiver>();
    private IReward _reward;
    private RewardType _rewardType;

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnCick);
    }

    public void Init(LevelUpWindow levelUpWindow, List <IRewardGiver> rewardGiver = null)
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
        _rewardType = _reward.GetRewardType();
        _attackLabel.text = _reward.Title;
        gameObject.SetActive(true);
    }

    private void OnCick()
    {
        if (_reward != null && _rewardGiver != null)
            foreach (var rewardGiver in _rewardGiver)
            {
                rewardGiver.GiveReward(_reward);
            }
           // _rewardGiver.GiveReward(_reward);
        _levelUpWindow.CloseLevelUpWindow();
    }
}
