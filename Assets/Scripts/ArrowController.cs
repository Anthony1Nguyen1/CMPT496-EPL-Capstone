/* Description: Script that listens for tap gestures and triggers item cycling accordingly. */

using UnityEngine;
using TouchScript.Gestures;

public class ArrowController : MonoBehaviour
{
    public ItemController item;             // The item.
    public enum ArrowDirection { Up, Down } // Enum for the two different arrow types.
    public ArrowDirection direction;
    [SerializeField] private AudioClip _pressSound;
    private AudioSource audioSource;

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

    private void OnEnable() { GetComponent<TapGesture>().Tapped += TappedHandler; }
    private void OnDisable() { GetComponent<TapGesture>().Tapped += TappedHandler; }

    // Purpose: Main function for controlling cycling behaviour. Calls upon item methods depending on the arrow direction.
    // Params: sender, e
    // Return: void
    private void TappedHandler(object sender, System.EventArgs e)
    {
        if (_pressSound != null && audioSource != null)
        {
            audioSource.Play();
        }

        if      (direction == ArrowDirection.Up) { item.CycleUp(); }
        else if (direction == ArrowDirection.Down) { item.CycleDown(); }
    }
}
