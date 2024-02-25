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
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }

        pressGesture = volumeSlider.GetComponent<PressGesture>();
        releaseGesture = volumeSlider.GetComponent<ReleaseGesture>();

        pressGesture.Pressed += OnSliderPressed;
        releaseGesture.Released += OnSliderReleased;
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
            musicAudioSource.volume = volumeSlider.value;
            Save();
        }
    }

    private void Load() 
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
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
}
