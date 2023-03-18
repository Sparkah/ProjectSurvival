using _ProjectSurvival.Infrastructure;
using _ProjectSurvival.Scripts.Audio;
using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Gold
{
    public class GoldHolder : MonoBehaviour
    {
        private float _goldAmountToDrop;

        [Inject]
        private World _world;
        

        private void Start()
        {
        }

        public void SetUp(float amount)
        {
            _goldAmountToDrop = amount;
        }

        public void DropGold()
        {
            _world.Gold.Value += _goldAmountToDrop;
            AudioPlayer.Audio.PlaySound(AudioSounds.Coins);
        }
    }
}
