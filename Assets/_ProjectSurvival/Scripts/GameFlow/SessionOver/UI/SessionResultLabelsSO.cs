using UnityEngine;

namespace _ProjectSurvival.Scripts.GameFlow.SessionOver
{
    [CreateAssetMenu(fileName = "NewSessionResultLabels", menuName = "Survivors prototype/Session result labels", order = 1)]

    public class SessionResultLabelsSO : ScriptableObject
    {
        [SerializeField] SessionResult _sessionResult;
        [SerializeField] private string _titleLabelText;
        [SerializeField, TextArea] private string _descriptionLabelText;

        public SessionResult SessionResult => _sessionResult;
        public string TitleLabelText => _titleLabelText;
        public string DescriptionLabelText => _descriptionLabelText;
    }
}
