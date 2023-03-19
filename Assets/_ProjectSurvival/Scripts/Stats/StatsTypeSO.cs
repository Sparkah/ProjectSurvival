using _ProjectSurvival.Scripts.LevelingSystem.Rewards;
using UnityEngine;

[CreateAssetMenu(fileName = "New stat type", menuName = "Survivors prototype/Stat type", order = 1)]
public class StatsTypeSO : ScriptableObject, IReward
{
    [SerializeField] private string _title;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _picture;
    [SerializeField] private int _statAmount;

    public string Title => _title;
    public string Description => _description;
    public Sprite Picture => _picture;

    public RewardType GetRewardType()
    {
        return RewardType.Stat;
    }
}