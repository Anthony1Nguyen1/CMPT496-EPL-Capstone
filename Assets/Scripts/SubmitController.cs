using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;

public class SubmitController : MonoBehaviour
{
    public GameObject[] columns; // The four categories/containers of items.
    private int[] pattern; // The predetermined recipe.

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
            var index = ingredient.currentIndex;
            var ingredientName = ingredient.dotPool[index].name;
            var ingredientChosen = ingredient.itemChosen;
            if (!ingredientChosen)
            {
                return false;
            }
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
        print("Try button tapped!");
        if (IsReadyForSubmit())
        {
            print("Good for submit!");
        }
        else
        {
            print("Not ready to submit!");
        }
    }

}
