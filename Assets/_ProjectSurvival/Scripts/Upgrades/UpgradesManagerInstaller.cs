using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Upgrades
{
    public class UpgradesManagerInstaller : MonoInstaller
    {
        [SerializeField] private UpgradesManager _upgradesManager;
        
        public override void InstallBindings()
        {
            Container.Bind<UpgradesManager>().FromInstance(_upgradesManager).AsSingle();
        }
    }
}