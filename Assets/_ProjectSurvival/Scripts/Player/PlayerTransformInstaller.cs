using _ProjectSurvival.Scripts.Player;
using UnityEngine;
using Zenject;

public class PlayerTransformInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Player player = FindObjectOfType<Player>(); // = No need to link reference by hands.
        if (player)
        {
            Container.Bind<Transform>().WithId("Player").FromInstance(player.transform).AsSingle();
            Container.Bind<Player>().FromInstance(player).AsSingle();
        }
    }
}
