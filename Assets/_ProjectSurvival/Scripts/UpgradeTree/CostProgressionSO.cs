using UnityEngine;

namespace _ProjectSurvival.Scripts.UpgradeTree
{
    [CreateAssetMenu(fileName = "Cost progression", menuName = "Survivors prototype/Cost progression", order = 1)]
    public class CostProgressionSO : ScriptableObject
    {
        [SerializeField] private int[] _costProgression;
    
        public int[] CostProgression => _costProgression;
    }
}
