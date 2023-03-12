using _ProjectSurvival.Infrastructure;
using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Gold
{
    public class GoldHolder : MonoBehaviour
    {
        private float _goldAmountToDrop;
        
        [Inject]
        private World _world;

        public void SetUp(float amount)
        {
            _goldAmountToDrop = amount;
        }

        public void DropGold()
        {
            _world.Gold.Value += _goldAmountToDrop;
            Debug.Log($"Total gold: {_world.Gold.Value}");
        }
    }
}
