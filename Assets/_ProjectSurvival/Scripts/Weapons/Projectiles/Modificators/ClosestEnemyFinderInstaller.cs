using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Weapons.Projectiles.Modificators
{
    public class ClosestEnemyFinderInstaller : MonoInstaller
    {
        [SerializeField] private ClosestEnemyFinder _finder;
        public override void InstallBindings()
        {
            Container.Bind<ClosestEnemyFinder>().FromInstance(_finder).AsSingle();
        }
    }
}
