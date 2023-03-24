using UnityEngine;

namespace _ProjectSurvival.Scripts.GameFlow.SessionOver
{
    [CreateAssetMenu(fileName = "AvailableSessionResultLabels", menuName = "Survivors prototype/Available session result labels", order = 1)]
    public class AvailableSessionResultLabelsSO : ScriptableObject
    {
        [SerializeField] private SessionResultLabelsSO[] _sessionResultLabelsSOs;

        public SessionResultLabelsSO FindLabelsFor(SessionResult sessionResult)
        {
            for (int i = 0; i < _sessionResultLabelsSOs.Length; i++)
            {
                if (_sessionResultLabelsSOs[i].SessionResult == sessionResult)
                    return _sessionResultLabelsSOs[i];
            }
            return null;
        }
    }
}
