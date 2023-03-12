using UnityEngine;

namespace _ProjectSurvival.Scripts.Gold
{
    public class GoldHolder : MonoBehaviour
    {
        private float _goldAmountToDrop;

        public void SetUp(float amount)
        {
            _goldAmountToDrop = amount;
        }

        public void DropGold()
        {
            Debug.Log($"{_goldAmountToDrop} Gold dropped ");
        }
    }
}
