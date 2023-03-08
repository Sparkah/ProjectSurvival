using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Weapons.WeaponTypes
{
    public class WeaponTypeSelectorInstaller : MonoInstaller
    {
        [SerializeField] private WeaponTypeSelector _weaponTypeSelector;
        public override void InstallBindings()
        {
            Container.Bind<WeaponTypeSelector>().FromInstance(_weaponTypeSelector).AsSingle();
        }
    }
}
