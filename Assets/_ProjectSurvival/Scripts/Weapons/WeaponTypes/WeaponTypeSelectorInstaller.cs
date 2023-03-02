using UnityEngine;
using Zenject;

public class WeaponTypeSelectorInstaller : MonoInstaller
{
    [SerializeField] private WeaponTypeSelector _weaponTypeSelector;
    public override void InstallBindings()
    {
        Container.Bind<WeaponTypeSelector>().FromInstance(_weaponTypeSelector).AsSingle();
    }
}
