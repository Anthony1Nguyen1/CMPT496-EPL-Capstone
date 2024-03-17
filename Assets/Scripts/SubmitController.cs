/* Description: Script on the "TRY" button (acts as a go-between for the HistoryController and ItemController scripts). */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TouchScript.Gestures;
using UnityEngine;
using UnityEngine.UI;

public class SubmitController : MonoBehaviour
{
    // Main objects
    [SerializeField] private GameObject[] items;                  // The four items selected by the user.
    [SerializeField] private HistoryController historyController; // Reference to the history pane.
    [SerializeField] private int[] indices;                       // The indices of the items chosen.

    // Audio/sound
    [SerializeField] private AudioClip _pressSound;
    [SerializeField] private AudioSource audioSource;

    // Submit buttons
    [SerializeField] private Image _img;                           // Reference to the image component
    [SerializeField] private Sprite _defaultSprite, _pressedSprite; // Sprites for default and pressed states
    private bool canTap = true; // Flag to track if the image can be tapped

    // Animations
    [SerializeField] private SubmitAnimations SubmitAnimations;
    [SerializeField] private RightCanvasAnimations _rightCanvasAnimations;
    [SerializeField] private ScreenFade screenFade;     // Screen fade animation
    [SerializeField] private float cooldownTime = 2f; // Cooldown time in seconds
    // [SerializeField] private WinAnimations WinAnimations;

    // Game state
    public bool isGameWon { get; private set; }
    [SerializeField] private int tryNumber;

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

    // Purpose: Boolean check for whether a full pattern has been selected by the user in the main panel.
    // Params: none
    // Return: true if all boxes have been filled, false otherwise.
    private bool IsReadyForSubmit()
    {
        return items.All(item => item.activeSelf);
    }

    // Purpose: Gets the active sprites and their GameObjects.
    // Params: none
    // Return: List of (Sprites, source objects).
    private List<(Sprite sprite, GameObject source)> GetActiveItems()
    {
        var activeItems = new List<(Sprite sprite, GameObject source)>();
        indices         = new int[items.Length];

        var i = 0;
        foreach (var item in items)
        {
            var image = item.GetComponent<Image>();
            activeItems.Add((image.sprite, item));
            indices[i] = item.GetComponent<ItemController>().CurrentIndex;
            i++;
        }

        return activeItems;
    }

    // Purpose: Calls the deactivate dots function on the item script (for each item frame).
    // Params: none
    // Return: void
    private void DeactivateDots()
    {
        foreach (var item in items)
        {
            item.GetComponent<ItemController>().DeactivateDots();
        }
    }

    // Purpose: Starts the move by
    // Params: none
    // Return: void
    private void StartAnimations()
    {
        var activeItems = GetActiveItems();
        var sprites     = activeItems.Select(item => item.sprite).ToList();
        var sources     = activeItems.Select(item => item.source).ToList();

        SubmitAnimations.AnimateActiveSprites(sprites, sources, tryNumber, historyController.transform);
        _rightCanvasAnimations.AnimateRightAnimations(indices);
    }

    // Purpose: Checks if the game has been won. Disables submit button on win.
    // Params: none
    // Return: IEnumerator
    private IEnumerator UpdateState()
    {
        yield return new WaitForSeconds(1.3f);

        isGameWon = historyController.GetResult(indices, tryNumber).GameWon;
        tryNumber++;
    }

    // Purpose: Main logic for submitting game state.
    // Params: none
    // Return: void
    private IEnumerator Submit()
    {
        // Button flip when submit is pressed.
        _img.sprite = _pressedSprite;
        yield return new WaitForSeconds(0.1f);
        _img.sprite = _defaultSprite;

        if (IsReadyForSubmit())
        {
            // screenFade.FadeInOut();

            // Start the animations.
            StartAnimations();

            // Wait for game state to be checked.
            yield return StartCoroutine(UpdateState());

            // Deactivate submit button upon win or loss.
            if (isGameWon || tryNumber == 12) { gameObject.SetActive(false); yield break; }

            // Otherwise, get ready for next move.
            DeactivateDots();
        }
        else { Debug.Log("Not ready to submit!"); }
    }

    private IEnumerator SubmitWithCooldown()
    {
        // Disable tapping on the submit image
        canTap = false;

        // Wait for the cooldown period
        yield return new WaitForSeconds(cooldownTime);

        // Enable tapping on the submit image after the cooldown period
        canTap = true;

        // Proceed with the submit logic here
        // For example, you can call another method to handle the submit action
        // HandleSubmit();
    }

    // private void HandleSubmit()
    // {
    //     // Implement your submit logic here
    //     // This method will be called after the cooldown period
    //     Debug.Log("Submit action executed!");
    // }

    private void OnEnable() { GetComponent<TapGesture>().Tapped += TappedHandler; }
    private void OnDisable() { GetComponent<TapGesture>().Tapped -= TappedHandler; }

    // Purpose: Main function for controlling submit behaviour.
    // Params: sender, e
    // Return: void
    private void TappedHandler(object sender, System.EventArgs e)
    {
        if (!canTap) return; // If tapping is not allowed, exit the method
        // If tapping is allowed, proceed with the tap logic
        StartCoroutine(SubmitWithCooldown());

        if (_pressSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(_pressSound);
        }

        StartCoroutine(Submit());
    }
}
