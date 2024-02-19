using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TouchScript.Gestures;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    private PressGesture pressGesture;
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

        pressGesture.Pressed += OnSliderPressed;
    }

    private void OnDestroy()
    {
        pressGesture.Pressed -= OnSliderPressed;
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
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
        ChangeVolume();
    }
}
