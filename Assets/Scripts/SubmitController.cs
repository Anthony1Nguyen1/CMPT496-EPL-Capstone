/* Description: Script on the "TRY" button (acts as a go-between for the HistoryController and ItemController scripts). */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TouchScript.Gestures;
using UnityEngine;
using UnityEngine.UI;

public class SubmitController : MonoBehaviour
{
    [SerializeField] private GameObject[] items;                  // The four items selected by the user.
    [SerializeField] private HistoryController historyController; // Reference to the history pane.
    [SerializeField] private int[] indices;                       // The indices of the items chosen.
    [SerializeField] private AudioClip _pressSound;               // Sound
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Image _img;                           // Reference to the image component
    [SerializeField] private Sprite _defaultSprite, _pressedSprite; // Sprites for default and pressed states
    [SerializeField] private ScreenFade screenFade;
    [SerializeField] private GameObject[] cauldrons;
    // [SerializeField] private WinAnimations WinAnimations;
    [SerializeField] private SubmitAnimations SubmitAnimations;

    public bool GameWon { get; private set; }                     // Flag that checks if game has been won.
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

    // Purpose: Gets the active sprites.
    // Params: none
    // Return: List of Sprites.
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

    // Purpose: Calls the submit function on the history script and increments the try number. Disables TRY button on win.
    // Params: none
    // Return: void
    private void SubmitMove()
    {
        var activeItems = GetActiveItems();
        var sprites = activeItems.Select(item => item.sprite).ToList();
        var sources = activeItems.Select(item => item.source).ToList();

        StartCoroutine(AnimateAndSubmit(sprites, sources));
    }

    private IEnumerator AnimateAndSubmit(List<Sprite> sprites, List<GameObject> sources)
    {
        SubmitAnimations.AnimateActiveSprites(sprites, sources, tryNumber, historyController.transform);

        // Wait for one second before displaying results.
        yield return new WaitForSeconds(1.3f);
        GameWon = historyController.GetResult(indices, tryNumber).GameWon;
        tryNumber++;

        if (GameWon)
        {
            gameObject.SetActive(false);
            // WinAnimations.PlayAnimations();
        }

        // Show the corresponding cauldron and hide the others
        var selectedCauldronIndex = indices[0]; // Assuming the first item determines the cauldron
        for (int i = 0; i < cauldrons.Length; i++)
        {
            if (i == selectedCauldronIndex)
            {
                cauldrons[i].SetActive(true);
            }
            else
            {
                cauldrons[i].SetActive(false);
            }
        }
    }

    // Purpose: Main logic for submitting game state.
    // Params: none
    // Return: void
    private IEnumerator ChangeSpriteTemporarily()
    {
        // Change sprite to pressed sprite
        _img.sprite = _pressedSprite;

        // Wait for a short duration
        yield return new WaitForSeconds(0.1f); // Adjust as needed

        // Revert back to default sprite
        _img.sprite = _defaultSprite;

        if (IsReadyForSubmit())
        {
            screenFade.FadeInOut();
            SubmitMove();
            if (GameWon || tryNumber == 12) { gameObject.SetActive(false); yield break; } // Win/loss will disable button.
            DeactivateDots();
        }
        else
        {
            Debug.Log("Not ready to submit!");
        }
    }

    private void OnEnable() { GetComponent<TapGesture>().Tapped += TappedHandler; }
    private void OnDisable() { GetComponent<TapGesture>().Tapped -= TappedHandler; }

    // Purpose: Main function for controlling submit behaviour.
    // Params: sender, e
    // Return: void
    private void TappedHandler(object sender, System.EventArgs e)
    {
        if (_pressSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(_pressSound);
        }

        StartCoroutine(ChangeSpriteTemporarily());
    }
}
