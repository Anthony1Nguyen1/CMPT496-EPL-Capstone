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
    }

}
