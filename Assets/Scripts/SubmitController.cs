using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;

public class SubmitController : MonoBehaviour
{
    public GameObject[] columns; // The four categories/containers of items.
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
        foreach (var element in columns)
        {
            var ingredient = element.GetComponent<ItemController>();
            var ingredientChosen = ingredient.itemChosen;
            if (!ingredientChosen) { return false; }
        }
        return true;
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
        print(IsReadyForSubmit() ? "Good for submit!" : "Not ready to submit!");
    }

}