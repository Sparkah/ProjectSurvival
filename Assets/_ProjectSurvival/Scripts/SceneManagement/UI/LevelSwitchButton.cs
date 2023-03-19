using UnityEngine;
using UnityEngine.UI;

namespace _ProjectSurvival.Scripts.SceneManagement.UI
{
    /// <summary>
    /// Button for loading selected level.
    /// </summary>
    public class LevelSwitchButton : MonoBehaviour
    {
        [SerializeField] private LevelSO _levelSO;
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClickSwitch);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClickSwitch);
        }

        private void OnClickSwitch()
        {
            LevelLoader levelLoader = new LevelLoader();
            levelLoader.LoadLevel(_levelSO);
        }
    }
}
