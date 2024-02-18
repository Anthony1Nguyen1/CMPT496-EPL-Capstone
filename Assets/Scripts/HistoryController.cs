/* Description: Script that controls the history panel. */

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HistoryController : MonoBehaviour
{
    [SerializeField] private Sprite winSprite;
    [SerializeField] private Sprite[] guessesBoxSprites;
    [SerializeField] private int[] pattern;

    // Purpose: Generates the winning pattern (proper indices) of items for the game.
    // Params: none
    // Return: void
    private void Start()
    {
        // List declarations for winning pattern and chosen.
        pattern = new int[4];
        for (var i = 0; i < 4; i++)
        {
            pattern[i] = (Random.Range(0, 4));
        }
        print("Pattern is: " + string.Join(", ", pattern));

    }

    // Purpose: Fills a row/move in the history panel with whatever the user submitted.
    // Params: items: array of user-selected items
    //         tryNumber: the number of tries the user has attempted so far.
    // Return: void
    public void Submit(List<GameObject> items, int tryNumber, int[] indices)
    {
        var row = transform.GetChild(tryNumber); // Get the corresponding row in the history panel.
        for (var i = 0; i < items.Count; i++)
        {
            var item     = items[i];                                         // Item to be copied.
            var rowFrame = row.GetChild(i);                                  // Square box for the new item
            var newItem  = Instantiate(item, rowFrame, false); // Copy the item over.
            newItem.SetActive(true);                                         // Make the new item active.
        }

        var correctGuesses = GetCorrectNumberOfItems(indices);
        row.GetChild(4).GetComponent<Image>().sprite = guessesBoxSprites[correctGuesses];
    }

    private int GetCorrectNumberOfItems(int[] indices)
    {
        var correctSoFar = 0;
        for (var i = 0; i < pattern.Length; i++)
        {
            if (pattern[i] == indices[i]) { correctSoFar++; }
        }

        return correctSoFar;
    }

    private bool CheckIfWon(int[] indices, GameObject historyPanel)
    {
        var correctSoFar = GetCorrectNumberOfItems(indices);
        print($"Correct guesses: {correctSoFar}");
        if (correctSoFar != 4) return false;
        historyPanel.GetComponent<Image>().sprite = winSprite;
        return true;
    }
}
