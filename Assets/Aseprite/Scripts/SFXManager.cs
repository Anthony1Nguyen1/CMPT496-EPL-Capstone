using UnityEngine;
using UnityEngine.UI;
using TouchScript.Gestures;

public class SFXManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    private PressGesture pressGesture;
    private ReleaseGesture releaseGesture;
    private bool isDragging = false;
    public AudioSource[] sfxAudioSource;

    void Start()
    {
        pressGesture = volumeSlider.GetComponent<PressGesture>();
        releaseGesture = volumeSlider.GetComponent<ReleaseGesture>();

        pressGesture.Pressed += OnSliderPressed;
        releaseGesture.Released += OnSliderReleased;

        volumeSlider.value = Settings.SFXVolume;
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
            Settings.SFXVolume = volumeSlider.value;
            ApplySettings();
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
        foreach (var audioSource in sfxAudioSource)
        {
            audioSource.volume = Settings.SFXVolume;
        }
    }
}
