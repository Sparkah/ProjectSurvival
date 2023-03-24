using _ProjectSurvival.Scripts.Enemies;
using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.GameFlow.Statistics
{
    public class SessionOverDataUI : MonoBehaviour
    {
        [SerializeField] private EnemyLevelResultUI[] _enemyLevelResultUIs;
        [SerializeField] private EnemyTypeSO _exampleEnemyDataSO;
        [SerializeField] private StatisticValueUI _bulletsStatistics;
        [SerializeField] private StatisticValueUI _defeatedStatistics;
        [SerializeField] private StatisticValueUI _earnedStatistics;
        private SessionStatistics _gameStatistics;

        [Inject]
        private void Construct(SessionStatistics gameStatistics)
        {
            _gameStatistics = gameStatistics;
        }

        public void UpdateData(int additionalCurrency = 0)
        {
            int defeatedAmount;
            float price;
            float enemyCurrency;

            float totalCurrency = 0;
            int totalDefeated = 0;

            for (int i = 0; i < _enemyLevelResultUIs.Length; i++)
            {
                defeatedAmount = _gameStatistics.ReadEnemy(i + 1);
                price = _exampleEnemyDataSO.GetEnemyLevelData(i + 1).BaseGold;
                enemyCurrency = defeatedAmount * price;
                _enemyLevelResultUIs[i].FillData(defeatedAmount, price, enemyCurrency);

                totalCurrency += enemyCurrency;
                totalDefeated += defeatedAmount;
            }

            _earnedStatistics.FillData(totalCurrency + additionalCurrency);
            _defeatedStatistics.FillData(totalDefeated);
            _bulletsStatistics.FillData(_gameStatistics.ReadStatistic(StatisticType.FiredProjectiles));
        }
    }
}
