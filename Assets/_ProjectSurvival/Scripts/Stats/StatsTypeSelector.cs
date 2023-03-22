using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Stats
{
    public class StatsTypeSelector : MonoBehaviour
    {
        [Inject] private ActiveStats _activeStats;
        [SerializeField] private AvailableStatsSO _availableStatsSO;

        public StatsTypeSO[] SelectStats(int count)
        {
           StatsTypeSO[] availableWeapons = _availableStatsSO.SelectAvailableStats(_activeStats);

            RandomRangeSelector randomRangeSelector = new RandomRangeSelector();
            int[] selectedIndexes = randomRangeSelector
                .SelectRandomIndexes(count, availableWeapons.Length);

            StatsTypeSO[] selectedstats = new StatsTypeSO[selectedIndexes.Length];
            for (int i = 0; i < selectedIndexes.Length; i++)
                selectedstats[i] = availableWeapons[selectedIndexes[i]];

            return selectedstats;
        }
    }
}