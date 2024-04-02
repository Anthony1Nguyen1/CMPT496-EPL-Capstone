/* Description: Script that listens for tap gestures and triggers item cycling accordingly. */

using UnityEngine;
using TouchScript.Gestures;

public class ArrowController : MonoBehaviour
{
    [SerializeField] public ItemController itemController;
    [SerializeField] private SubmitController submitController;
    [SerializeField] public ArrowDirection direction;
    [SerializeField] private AudioClip _pressSound;
    [SerializeField] private AudioSource audioSource;

    private bool arrowEnabled = true;

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
    public enum ArrowDirection { Upwards, Downwards } // Enum for the two different arrow types.

    private void OnEnable() { GetComponent<TapGesture>().Tapped += TappedHandler; }
    private void OnDisable() { GetComponent<TapGesture>().Tapped -= TappedHandler; }

    // Purpose: Main function for controlling cycling behaviour. Calls upon item methods depending on the arrow direction.
    // Params: sender, e
    // Return: void
    private void TappedHandler(object sender, System.EventArgs e)
    {
        if (_pressSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(_pressSound);
        }

        if (submitController.isGameWon == false)
        {
            if      (direction == ArrowDirection.Upwards) { itemController.CycleUp(); }
            else if (direction == ArrowDirection.Downwards) { itemController.CycleDown(); }
        }
    }
}
