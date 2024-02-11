/* Description: Script on the "TRY" button (acts as a go-between for the HistoryController and ItemController scripts). */

using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;

public class SubmitController : MonoBehaviour
{
    public GameObject[] items; // The four items selected by the user.
    public GameObject history; // Reference to the history pane.
    private int[] _pattern;     // The predetermined recipe.
    private int _tryNumber = 0;

    // Desc: Generates the winning pattern (proper indices) of items for the game.
    // Params: none
    // Return: void
    private void Start()
    {
        // Winning pattern.
        _pattern = new int[4];
        for (var i = 0; i < _pattern.Length; i++)
        {
            _pattern[i] = Random.Range(0, 4);
        }
    }

    // Desc: Boolean check for whether a full pattern has been selected by the user in the main panel.
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

            if (activeChild == null) { return false; }
        }

        return true;
    }

    // Desc: Gets the active prefab within the frame.
    // Params: none
    // Return: The actual objects the user has selected for submission.
    private List<GameObject> GetActiveChildren()
    {
        List<GameObject> activeChildren = new List<GameObject>();

        foreach (var item in items)
        {
            foreach (Transform child in item.transform)
            {
                if (child.gameObject.activeInHierarchy) { activeChildren.Add(child.gameObject); break; }
            }
        }

        return activeChildren;
    }

    // Desc: Calls the submit function on the history script and increments the try number.
    // Params: none
    // Return: void
    private void SubmitMove()
    {
        history.GetComponent<HistoryController>().Submit(GetActiveChildren(), _tryNumber);
        _tryNumber++;
    }

    // Desc: Calls the deactivate dots function on the item script (for each item).
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
    private void OnDisable() { GetComponent<TapGesture>().Tapped += TappedHandler; }

    // Desc: Main function for controlling submit behaviour.
    // Params: sender, e
    // Return: void
    private void TappedHandler(object sender, System.EventArgs e)
    {
        if (IsReadyForSubmit())
        {
            SubmitMove();
            Debug.Log("Submitted successfully!");
            DeactivateDots();
        }
        else
        {
            Debug.Log("Not ready to submit!");
        }
    }

}
