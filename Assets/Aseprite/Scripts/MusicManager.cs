using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TouchScript.Gestures;

public class MusicManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    private PressGesture pressGesture;
    private ReleaseGesture releaseGesture;
    private bool isDragging = false;
    public AudioSource musicAudioSource;

    private void Start()
    {
        pressGesture = volumeSlider.GetComponent<PressGesture>();
        releaseGesture = volumeSlider.GetComponent<ReleaseGesture>();

        pressGesture.Pressed += OnSliderPressed;
        releaseGesture.Released += OnSliderReleased;

        Settings.MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        volumeSlider.value = Settings.MusicVolume;

        ApplySettings();
    }

    private void OnDestroy()
    {
        pressGesture.Pressed -= OnSliderPressed;
        releaseGesture.Released -= OnSliderReleased;
    }

    public void ChangeVolume()
    {
        if (!isDragging)
        {
            Settings.MusicVolume = volumeSlider.value;

            ApplySettings();

            PlayerPrefs.SetFloat("MusicVolume", Settings.MusicVolume);
            PlayerPrefs.Save();
        }
    }

    private void OnSliderPressed(object sender, System.EventArgs e)
    {
        isDragging = true;
        ChangeVolume();
    }

    private void OnSliderReleased(object sender, System.EventArgs e)
    {
        isDragging = false;
        ChangeVolume();
    }

    private void ApplySettings()
    {
        musicAudioSource.volume = Settings.MusicVolume;
    }
}