/* Description: Script on the "TRY" button (acts as a go-between for the HistoryController and ItemController scripts). */

using System.Collections;
using System.Collections.Generic;
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
    public bool GameWon { get; private set; }                     // Flag that checks if game has been won.
    private int tryNumber;

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
        // Iterate over each item.
        foreach (var item in items)
        {
            GameObject activeChild = null;

            // Iterate over the children of the item.
            foreach (Transform child in item.transform)
            {
                if (child.gameObject.activeInHierarchy)
                {
                    activeChild = child.gameObject;
                    break;
                }
            }

            // If we hit this line, we know there is an inactive child.
            if (activeChild == null) { return false; }
        }

        return true;
    }

    // Purpose: Gets the active prefab within the frame.
    // Params: none
    // Return: The actual objects the user has selected for submission.
    private List<GameObject> GetActiveChildren()
    {
        var activeChildren = new List<GameObject>();

        indices = new int[items.Length];

        for(var i = 0; i < items.Length; i++)
        {
            foreach (Transform child in items[i].transform)
            {
                if (child.gameObject.activeInHierarchy)
                {
                    activeChildren.Add(child.gameObject);

                    var index = items[i].GetComponent<ItemController>().CurrentIndex;
                    indices[i] = index;
                    break;
                }
            }
        }

        return activeChildren;
    }

    // Purpose: Calls the submit function on the history script and increments the try number. Disables TRY button on win.
    // Params: none
    // Return: void
    private void SubmitMove()
    {
        GameWon = historyController.Submit(GetActiveChildren(), tryNumber, indices).GameWon;
        tryNumber++;
        if (GameWon) { gameObject.SetActive(false); }
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

    // Coroutine to temporarily change sprite
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
            SubmitMove();
            if (GameWon || tryNumber == 12) { gameObject.SetActive(false); yield break; } // Win/loss will disable button.
            DeactivateDots();
        }
        else
        {
            Debug.Log("Not ready to submit!");
        }
    }
}
