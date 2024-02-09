using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;

public class SubmitController : MonoBehaviour
{
    public GameObject[] items; // The four categories/containers of items.
    public GameObject[] moves; // The moves to be placed in the history pane.
    private int[] pattern; // The predetermined recipe.
    private int tryNumber = 1;

    private void Start()
    {
        // Winning pattern.
        pattern = new int[4];
        for (var i = 0; i < pattern.Length; i++)
        {
            pattern[i] = Random.Range(0, 4);
            Debug.Log("Item "+ i + " is: " + pattern[i]);
        }
    }

    private bool IsReadyForSubmit()
    {
        foreach (var item in items)
        {
            var ingredient = item.GetComponent<ItemController>();
            var ingredientChosen = ingredient.itemChosen;
            if (!ingredientChosen) { return false; }
        }
        return true;
    }

    private void SubmitMove()
    {
        // Temporary array to hold player's submitted move.
        var submittedItems = new GameObject[items.Length];

        // Copy player's move into temporary array.
        for (var i = 0; i < items.Length; i++)
        {
            var ingredient = items[i].GetComponent<ItemController>();
            submittedItems[i] = ingredient.gameObject;
        }

        Debug.Log($"Current row: {tryNumber}");
        var moveRow = moves[tryNumber];
        Debug.Log($"Current row object: {moves[tryNumber]}");

        // Loop through the children of moveRow
        for (var i = 0; i < submittedItems.Length; i++)
        {
            var child = moveRow.transform.GetChild(i);
            Debug.Log($"Child {i}: {child}");
        }
    }

    private void OnEnable()
    {
        GetComponent<TapGesture>().Tapped += TappedHandler;
    }

    private void OnDisable()
    {
        GetComponent<TapGesture>().Tapped += TappedHandler;
    }

    private void TappedHandler(object sender, System.EventArgs e)
    {
        if (IsReadyForSubmit())
        {
            SubmitMove();
            Debug.Log("Submitted successfully!");
        }
        else
        {
            Debug.Log("Not ready to submit!");
        }
    }

}
