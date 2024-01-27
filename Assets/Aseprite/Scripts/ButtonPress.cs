using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TouchScript.Gestures;


public class ButtonSimulator : MonoBehaviour
{
    // Reference to the images and sprites
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _default, _pressed;

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
    }

    private void SimulateButtonUp()
    {
        // Change image to default when the button is "pressed"
        _img.sprite = _default;
    }
}