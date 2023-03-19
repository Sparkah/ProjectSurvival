using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Enemies.Evolution
{
    public class EnemiesEvolutionTrackerInstaller : MonoInstaller
    {
        [SerializeField] private EnemiesEvolutionTracker _enemiesEvolutionTracker;
        public override void InstallBindings()
        {
            Container.Bind<EnemiesEvolutionTracker>().FromInstance(_enemiesEvolutionTracker).AsSingle();
        }
    }
}
