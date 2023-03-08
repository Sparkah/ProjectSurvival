using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Stats
{
    public class StatsTypeSelectorInstaller : MonoInstaller
    {
        [SerializeField] private StatsTypeSelector _statsTypeSelector;
        public override void InstallBindings()
        {
            Container.Bind<StatsTypeSelector>().FromInstance(_statsTypeSelector).AsSingle();
        }
    }
}