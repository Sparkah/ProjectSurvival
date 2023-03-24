using UnityEngine;
using UnityEngine.UI;

namespace _ProjectSurvival.Scripts.GameFlow.SessionOver
{
    public class SessionResultUI : MonoBehaviour
    {
        [SerializeField] private Text _titleText;
        [SerializeField] private Text _descriptionText;

        public void FillData(SessionResultLabelsSO sessionResultLabelsSO)
        {
            _titleText.text = sessionResultLabelsSO.TitleLabelText;
            _descriptionText.text = sessionResultLabelsSO.DescriptionLabelText;
        }
    }
}
