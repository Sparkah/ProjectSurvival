using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Weapons.ActiveWeapons
{
    public class ActiveWeaponsInstaller : MonoInstaller
    {
        [SerializeField] private ActiveWeapons _activeWeapons;
        public override void InstallBindings()
        {
            Container.Bind<ActiveWeapons>().FromInstance(_activeWeapons).AsSingle();
        }
    }
}
