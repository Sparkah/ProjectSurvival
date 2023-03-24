using UnityEngine;

namespace _ProjectSurvival.Scripts.GameFlow.SessionOver
{
    [CreateAssetMenu(fileName = "AdditionalSessionBonus", menuName = "Survivors prototype/Additional session bonus", order = 1)]
    public class AdditionalSessionBonusSO : ScriptableObject
    {
        [SerializeField] private SessionResultBonus[] _resultBonuses;

        public int CalculateBonus(SessionResult sessionResult)
        {
            for (int i = 0; i < _resultBonuses.Length; i++)
            {
                if (_resultBonuses[i].SessionResult == sessionResult)
                    return _resultBonuses[i].Bonus;
            }
            return 0;
        }

        [System.Serializable]
        struct SessionResultBonus
        {
            [SerializeField] private SessionResult sessionResult;
            [SerializeField] private int bonus;

            public SessionResult SessionResult => sessionResult;
            public int Bonus => bonus;
        }
    }
}
