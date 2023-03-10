using UnityEngine;

namespace _ProjectSurvival.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerAttack _playerAttack;
        [SerializeField] private LevelableObject _levelableObject;
        [SerializeField] private DamagableObject _damagableObject;

        private void Start()
        {
            _levelableObject.Init();
            _playerAttack.StartFire();
            _damagableObject.SetupHealth(10);
        }
    }
}
