using System;
using _ProjectSurvival.Scripts.Audio;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //AudioPlayer.Audio.PlaySound(AudioSounds.Hoover);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioPlayer.Audio.PlayOneShotSound(AudioSounds.Enter);
    }
}
