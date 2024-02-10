using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;

public class SubmitController : MonoBehaviour
{
    public GameObject[] items; // The four items selected by the user.
    public GameObject history; // Reference to the history pane.
    private int[] pattern;     // The predetermined recipe.
    private int tryNumber = 0;

    private void Start()
    {
        // Winning pattern.
        pattern = new int[4];
        for (var i = 0; i < pattern.Length; i++)
        {
            pattern[i] = Random.Range(0, 4);
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
        // Copy player's move into temporary array.
        var submittedItems = new GameObject[items.Length];
        for (var i = 0; i < items.Length; i++)
        {
            var ingredient = items[i].GetComponent<ItemController>();
            submittedItems[i] = ingredient.gameObject;
        }

        history.GetComponent<HistoryController>().Submit(items, tryNumber);
        tryNumber++;
    }

    private void OnEnable()
    {
        GetComponent<TapGesture>().Tapped += TappedHandler;
    }

    private void OnDisable()
    {
        GetComponent<TapGesture>().Tapped += TappedHandler;
    }

    private void DeactivateDots()
    {
        foreach (var item in items)
        {
            item.GetComponent<ItemController>().DeactivateDots();
        }
    }
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
