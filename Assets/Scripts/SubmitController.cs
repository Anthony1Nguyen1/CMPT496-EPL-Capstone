/* Description: Main controller on the submit button. */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TouchScript.Gestures;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SubmitController : MonoBehaviour
{
    // Main objects
    [SerializeField] private HistoryController historyController; // Reference to the history pane.
    [SerializeField] private GameObject[] items;                  // The four items selected by the user.
    [SerializeField] private int[] indices;                       // The indices of the items chosen.

    // Audio/sound
    [SerializeField] private AudioClip _pressSound;
    [SerializeField] private AudioSource audioSource;

    // Submit buttons
    [SerializeField] private Image _img;                            // Reference to the image component
    [SerializeField] private Sprite _defaultSprite, _pressedSprite; // Sprites for default and pressed states
    private bool canTap = true;                                     // Flag to track if the image can be tapped

    // Animations
    [SerializeField] private SubmitAnimations SubmitAnimations;
    [SerializeField] private RightCanvasAnimations _rightCanvasAnimations;
    [SerializeField] private WinAnimation WinAnimation;
    [SerializeField] private ScreenFade screenFade;     // Screen fade animation
    [SerializeField] private WrongAnswerAnimation wrongAnswerAnimation;
    [SerializeField] private ResultsAnimation resultsAnimation;
    [SerializeField] private float cooldownTime = 2f; // Cooldown time in seconds

    // Game state
    public bool isGameWon { get; private set; }
    [SerializeField] private int tryNumber;

    // Difficulty level
    [SerializeField] private int maxTriesEasy = 8;
    [SerializeField] private int maxTriesHard = 12;
    private int maxTries;

    private void Start()
    {
        // Get or add AudioSource component
        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();

        // Assign the button press sound to the audio source
        audioSource.clip = _pressSound;

        maxTries = (SceneManager.GetActiveScene().name == "EasyDifficulty") ? maxTriesEasy : maxTriesHard;
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
        indices = new int[items.Length];

        for (var i = 0; i < items.Length; i++)
        {
            var item = items[i];
            var image = item.GetComponent<Image>();
            activeItems.Add((image.sprite, item));
            indices[i] = item.GetComponent<ItemController>().CurrentIndex;
        }

        return activeItems;
    }

    // Purpose: Calls the deactivate dots function on the item script (for each item frame).
    // Params: none
    // Return: void
    private void DeactivateItems()
    {
        foreach (var item in items) { item.GetComponent<ItemController>().DeactivateItems(); }
    }

    // Purpose: Starts the animations for item transformations, potions, and wrong answers.
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

    private void WinAnimations() { WinAnimation.AnimateWinAnimations(); }

    // Purpose: Checks if the game has been won. Disables submit button on win.
    // Params: none
    // Return: IEnumerator
    private IEnumerator UpdateState()
    {
        yield return new WaitForSeconds(1.3f);
        isGameWon = historyController.GetResult(indices, tryNumber).GameWon;
        tryNumber++;
    }

    private void StartWrongAnimations() { wrongAnswerAnimation.AnimateWrongAnswerAnimations(); }
    private void StopWrongAnimations() { wrongAnswerAnimation.StopAllCoroutines(); }

    private void ResultsAnimations() { resultsAnimation.PlayResultsAnimation(isGameWon);}

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
            screenFade.FadeInOut();
            yield return new WaitForSeconds(1.0f);

            // Game in play.
            if (!isGameWon || tryNumber != maxTries)
            {
                StartAnimations();
                StartWrongAnimations();
                DeactivateItems();
                yield return StartCoroutine(UpdateState());
            }

            // Game over.
            if (isGameWon || tryNumber == maxTries)
            {
                Debug.Log($"Game won? {isGameWon}");
                StopWrongAnimations();
                WinAnimations();
                DeactivateItems();
                ResultsAnimations();
            }
        }
        else { Debug.Log("Not ready to submit!"); }
    }

    private IEnumerator SubmitWithCooldown()
    {
        canTap = false;
        yield return new WaitForSeconds(cooldownTime);
        canTap = true;
    }

    private void OnEnable() { GetComponent<TapGesture>().Tapped += TappedHandler; }
    private void OnDisable() { GetComponent<TapGesture>().Tapped -= TappedHandler; }

    // Purpose: Main function for controlling submit behaviour.
    // Params: sender, e
    // Return: void
    private void TappedHandler(object sender, System.EventArgs e)
    {
        if (!canTap) return;

        StartCoroutine(SubmitWithCooldown());

        if (_pressSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(_pressSound);
        }

        StartCoroutine(Submit());
    }
}
