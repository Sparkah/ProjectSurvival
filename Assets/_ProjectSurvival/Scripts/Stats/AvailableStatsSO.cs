using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "AvailableStats", menuName = "Survivors prototype/Available stats", order = 1)]
public class AvailableStatsSO : ScriptableObject
{
    [SerializeField] private StatsTypeSO[] _statsTypeSOs;

    public StatsTypeSO[] SelectAvailableStats(/*ActiveStats activeWeapons*/)
    {
        List<StatsTypeSO> availableStats = new List<StatsTypeSO>();
        for (int i = 0; i < _statsTypeSOs.Length; i++)
        {
            //if (!activeWeapons.HasMaximumLevel(_weaponTypeSOs[i]))
                availableStats.Add(_statsTypeSOs[i]);
        }
        return _statsTypeSOs.ToArray();
    }
}