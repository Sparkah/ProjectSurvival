using System.Collections.Generic;
using UnityEngine;

namespace _ProjectSurvival.Scripts.GameFlow.Statistics
{
    public class SessionStatistics : MonoBehaviour
    {
        [SerializeField] private bool _resetOnStart;
        private Dictionary<StatisticType, int> _statistics = new Dictionary<StatisticType, int>();
        private Dictionary<int, int> _defeatedEnemies = new Dictionary<int, int>();

        private void Start()
        {
            if (_resetOnStart)
                ResetStatistics();
        }

        public void ResetStatistics()
        {
            _statistics.Clear();
            _statistics = new Dictionary<StatisticType, int>();
            _defeatedEnemies.Clear();
            _defeatedEnemies = new Dictionary<int, int>();
        }

        public void AddStatistic(StatisticType statisticType, int value)
        {
            if (statisticType == StatisticType.DefeatedEnemy)
                TrackEnemy(value);
            else
                TrackStat(statisticType, value);
        }

        public int ReadStatistic(StatisticType statisticType)
        {
            if (_statistics.TryGetValue(statisticType, out int value))
                return value;
            return 0;
        }

        public int ReadEnemy(int level)
        {
            if (_defeatedEnemies.TryGetValue(level, out int value))
                return value;
            return 0;
        }

        private void TrackStat(StatisticType statType, int amount)
        {
            if (_statistics.ContainsKey(statType))
                _statistics[statType] += amount;
            else
                _statistics.Add(statType, amount);
        }

        private void TrackEnemy(int level)
        {
            if (_defeatedEnemies.ContainsKey(level))
                _defeatedEnemies[level]++;
            else
                _defeatedEnemies.Add(level, 1);
        }
    }
}
