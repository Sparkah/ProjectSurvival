using System.Collections.Generic;
using System.Linq;
using _ProjectSurvival.Scripts.Stats;
using UnityEngine;

[CreateAssetMenu(fileName = "AvailableStats", menuName = "Survivors prototype/Available stats", order = 1)]
public class AvailableStatsSO : ScriptableObject
{
    [SerializeField] private StatsTypeSO[] _statsTypeSOs;

    public StatsTypeSO[] SelectAvailableStats(ActiveStats activeStats)
    {
        List<StatsTypeSO> availableStats = new List<StatsTypeSO>();
        for (int i = 0; i < _statsTypeSOs.Length; i++)
        {
            if (!activeStats.HasMaximumLevel(_statsTypeSOs[i]))
                availableStats.Add(_statsTypeSOs[i]);
        }
        return availableStats.ToArray();
    }

    public StatsTypeSO[] SelectAllStats()
    {
        return _statsTypeSOs.ToArray();
    }
}