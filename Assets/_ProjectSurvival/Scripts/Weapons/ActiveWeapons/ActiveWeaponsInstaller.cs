using UnityEngine;
using Zenject;

public class ActiveWeaponsInstaller : MonoInstaller
{
    [SerializeField] private ActiveWeapons _activeWeapons;
    public override void InstallBindings()
    {
        Container.Bind<ActiveWeapons>().FromInstance(_activeWeapons).AsSingle();
    }
}
