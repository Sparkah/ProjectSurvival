using UnityEngine;
using UnityEngine.UI;

public class LevelingBarUI : MonoBehaviour
{
    [SerializeField] private Slider _experienceSlider;
    [SerializeField] private Text _levelLabel;
    [SerializeField] private Behaviour _levelableObject;
    private ILevelable _levelable => _levelableObject as ILevelable;

    private void Start()
    {
        SetupUI();
        UpdateUI();
        _levelable.OnExperienceChanged += UpdateUI;
        _levelable.OnLevelUp += SetupUI;
    }

    private void OnDestroy()
    {
        _levelable.OnExperienceChanged -= UpdateUI;
        _levelable.OnLevelUp -= SetupUI;
    }

    private void SetupUI()
    {
        _experienceSlider.maxValue = _levelable.RequiredExperience;
        _levelLabel.text = _levelable.Level.ToString();
    }

    private void UpdateUI()
    {
        _experienceSlider.value = _levelable.CurrentExperience;
    }
}
