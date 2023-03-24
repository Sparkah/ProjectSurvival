using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.GameFlow.Statistics
{
    public class SessionStatisticsInstaller : MonoInstaller
    {
        [SerializeField] private SessionStatistics _gameStatistics;

        public override void InstallBindings()
        {
            Container.Bind<SessionStatistics>().FromInstance(_gameStatistics).AsSingle();
        }
    }
}
