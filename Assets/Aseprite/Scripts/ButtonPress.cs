using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TouchScript.Gestures;
using UnityEngine.SceneManagement;

public class ButtonSimulator : MonoBehaviour
{
    // Reference to the images and sprites
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _default, _pressed;
    [SerializeField] private AudioClip _pressSound;
    private AudioSource audioSource;

    public string MainCamera;
    private void Start()
    {
        // Get or add AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign the button press sound to the audio source
        audioSource.clip = _pressSound;
    }

    private void OnEnable()
    {
        GetComponent<PressGesture>().Pressed += OnPointerDown;
        GetComponent<ReleaseGesture>().Released += OnPointerUp;
    }

    private void OnDisable()
    {
        GetComponent<PressGesture>().Pressed -= OnPointerDown;
        GetComponent<ReleaseGesture>().Released -= OnPointerUp;
    }

    private void OnPointerDown(object sender, System.EventArgs e)
    {
        // Simulate button press when touch begins
        SimulateButtonDown();
    }

    private void OnPointerUp(object sender, System.EventArgs e)
    {
        // Simulate button release when touch ends
        SimulateButtonUp();
    }

    private void SimulateButtonDown()
    {
        // Change image to pressed when the button is "pressed"
        _img.sprite = _pressed;

        if (_pressSound != null && audioSource != null)
        {
            audioSource.Play();
        }
    }

    private void SimulateButtonUp()
    {
        // Change image to default when the button is "pressed"
        _img.sprite = _default;

        // Load a new scene
        if (!string.IsNullOrEmpty(MainCamera))
        {
            SceneManager.LoadScene(MainCamera);
        }
    }
}