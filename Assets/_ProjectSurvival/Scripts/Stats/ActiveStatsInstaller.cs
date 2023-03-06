using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Stats
{
    public class ActiveStatsInstaller : MonoInstaller
    {
        [SerializeField] private ActiveStats _activeStats;
        public override void InstallBindings()
        {
            Container.Bind<ActiveStats>().FromInstance(_activeStats).AsSingle();
        }
    }
}