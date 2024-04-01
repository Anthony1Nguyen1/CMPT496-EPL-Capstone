/* Description: Main controller on the submit button. */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SubmitController : ButtonPress
{
    // Main objects
    [SerializeField] private HistoryController historyController; // Reference to the history pane.
    [SerializeField] private GameObject[] items;                  // The four items selected by the user.
    [SerializeField] private int[] indices;                       // The indices of the items chosen.

    // Arrow and item controllers
    [SerializeField] private ArrowController[] arrowControllers;
    [SerializeField] private ItemController[] itemControllers;

    // Animations
    [SerializeField] private SubmitAnimations submitAnimations;
    [SerializeField] private RightCanvasAnimations rightCanvasAnimations;
    [SerializeField] private WinAnimation winAnimation;
    [SerializeField] private ScreenFade screenFade;     // Screen fade animation
    [SerializeField] private WrongAnswerAnimation wrongAnswerAnimation;
    [SerializeField] private ResultsAnimation resultsAnimation;
    [SerializeField] private float cooldownTime = 2f; // Cooldown time in seconds

    // Game state
    public bool isGameWon { get; private set; }
    [SerializeField] private int tryNumber;
    private bool canSubmit = true;

    // Difficulty level
    [SerializeField] private int maxTriesEasy = 8;
    [SerializeField] private int maxTriesHard = 12;
    private int maxTries;

    protected override void Start()
    {
        base.Start();
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

        submitAnimations.AnimateActiveSprites(sprites, sources, tryNumber, historyController.transform);
        rightCanvasAnimations.AnimateRightAnimations(indices);
    }

    private void WinAnimations()        { winAnimation.AnimateWinAnimations();                 }
    private void StartWrongAnimations() { wrongAnswerAnimation.AnimateWrongAnswerAnimations(); }
    private void StopWrongAnimations()  { wrongAnswerAnimation.StopAllCoroutines();            }
    private void ResultsAnimations()    { resultsAnimation.PlayResultsAnimation(isGameWon);    }

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

        // Prevent subsequent button presses while submit in progress.
        if (!canSubmit) { yield break; }
        DisableArrowAndItemFunctionality();

        if (IsReadyForSubmit())
        {
            // Disable button press for now
            canSubmit = false;

            // Screen fade
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

            // Game over by winning.
            if (isGameWon)
            {
                Debug.Log($"Game won? {isGameWon}");
                StopWrongAnimations();
                WinAnimations();
                DeactivateItems();
                ResultsAnimations();
            }

            // Game over by losing.
            if (tryNumber == maxTries)
            {
                Debug.Log($"Game won? {isGameWon}");
                StopWrongAnimations();
                DeactivateItems();
                ResultsAnimations();
            }
        }
        else { Debug.Log("Not ready to submit!"); }

        // Enable button presses again
        EnableArrowAndItemFunctionality();
        canSubmit = true;
    }

    protected override void SimulateButtonUp()
    {
        base.SimulateButtonUp();
        StartCoroutine(Submit());
    }

    private void DisableArrowAndItemFunctionality()
    {
        foreach (var arrowController in arrowControllers) { arrowController.SetSubmitInProgress(true); }
        foreach (var itemController in itemControllers)   { itemController.SetSubmitInProgress(true);  }
    }

    private void EnableArrowAndItemFunctionality()
    {
        foreach (var arrowController in arrowControllers) { arrowController.SetSubmitInProgress(false); }
        foreach (var itemController in itemControllers)   { itemController.SetSubmitInProgress(false);  }
    }
}
