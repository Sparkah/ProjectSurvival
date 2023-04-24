using _ProjectSurvival.Scripts.Enemies.Factory;
using UnityEngine;
using Zenject;

public class EnemyFactoryInstaller : MonoInstaller
{
    [SerializeField] private EnemyFactory _enemyFactory;
    public override void InstallBindings()
    {
        Container.Bind<EnemyFactory>().FromInstance(_enemyFactory).AsSingle();
    }
}
