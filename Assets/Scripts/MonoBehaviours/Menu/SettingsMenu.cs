using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Button acceptButton;
    [SerializeField] private Button backButton;
    [Space]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider lapsSlider;
    [Space]
    [SerializeField] private Text volumeValue;
    [SerializeField] private Text lapsValue;

    private float previousVolume;
    private float previousLaps;

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener((value) => volumeValue.text = value.ToString());
        lapsSlider.onValueChanged.AddListener((value) => lapsValue.text = value.ToString());

        acceptButton.onClick.AddListener(() => gameObject.SetActive(false));
        backButton.onClick.AddListener(() => gameObject.SetActive(false));

        acceptButton.onClick.AddListener(ApplySettings);
        backButton.onClick.AddListener(DiscardSettings);
    }

    private void ApplySettings()
    {
        AudioListener.volume = volumeSlider.value / 100f;
        GameManager.Instance.laps = (int)lapsSlider.value;
    }

    private void DiscardSettings()
    {
        volumeSlider.value = previousVolume;
        lapsSlider.value = previousLaps;
    }

    private void OnEnable()
    {
        volumeSlider.value = AudioListener.volume * 100;
        volumeValue.text = volumeSlider.value.ToString();
        previousVolume = volumeSlider.value;
        previousLaps = lapsSlider.value;
    }
}
