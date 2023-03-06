using System.Collections;
using System.Collections.Generic;
using _ProjectSurvival.Scripts.LevelingSystem.Rewards;
using UnityEngine;

public interface IRewardGiver 
{
    public void GiveReward(IReward reward);
}
