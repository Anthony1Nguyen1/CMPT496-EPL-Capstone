using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TouchScript.Gestures;
using UnityEngine.SceneManagement;

public class ButtonPress : MonoBehaviour
{
    // Reference to the images and sprites
    [SerializeField] protected Image _img;
    [SerializeField] protected Sprite _default, _pressed;
    [SerializeField] protected AudioClip _pressSound;
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected string MainCamera;

    protected virtual void Start()
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

    protected virtual void OnEnable()
    {
        GetComponent<PressGesture>().Pressed += OnPointerDown;
        GetComponent<ReleaseGesture>().Released += OnPointerUp;
    }

    private void OnDisable()
    {
        GetComponent<PressGesture>().Pressed -= OnPointerDown;
        GetComponent<ReleaseGesture>().Released -= OnPointerUp;
    }

    protected virtual void OnPointerDown(object sender, System.EventArgs e)
    {
        // Simulate button press when touch begins
        SimulateButtonDown();
    }

    protected virtual void OnPointerUp(object sender, System.EventArgs e)
    {
        // Simulate button release when touch ends
        SimulateButtonUp();
    }

    protected virtual void SimulateButtonDown()
    {
        // Change image to pressed when the button is "pressed"
        _img.sprite = _pressed;

        if (_pressSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(_pressSound);
        }
    }

    protected virtual void SimulateButtonUp()
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