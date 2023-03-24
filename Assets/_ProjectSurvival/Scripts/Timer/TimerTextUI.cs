using UnityEngine;

namespace _ProjectSurvival.Scripts.Timers
{
    public class TimerTextUI : TimerUI
    {
        [SerializeField] private TMPro.TMP_Text _timerLabel;

        protected override void UpdateUI(float time)
        {
            int seconds = Mathf.FloorToInt(time % 60);
            int minutes = Mathf.FloorToInt(time / 60f);
            _timerLabel.text = string.Concat(minutes.ToString("00"), ":", seconds.ToString("00"));
        }
    }
}
