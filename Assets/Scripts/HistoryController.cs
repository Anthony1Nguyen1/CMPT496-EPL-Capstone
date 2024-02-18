/* Description: Script that controls the history panel. */

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HistoryController : MonoBehaviour
{
    [SerializeField] private Sprite winSprite;

    // Purpose: Fills a row/move in the history panel with whatever the user submitted.
    // Params: items: array of user-selected items
    //         tryNumber: the number of tries the user has attempted so far.
    // Return: void
    public void Submit(List<GameObject> items, int tryNumber)
    {
        var row = transform.GetChild(tryNumber); // Get the corresponding row in the history panel.
        for (var i = 0; i < items.Count; i++)
        {
            var item     = items[i];                                         // Item to be copied.
            var rowFrame = row.GetChild(i);                                  // Square box for the new item
            var newItem  = Instantiate(item, rowFrame, false); // Copy the item over.
            newItem.SetActive(true);                                         // Make the new item active.
        }
    }

    private static int GetCorrectNumberOfItems(int[] pattern, int[] indices)
    {
        var correctSoFar = 0;
        for (var i = 0; i < pattern.Length; i++)
        {
            if (pattern[i] == indices[i]) { correctSoFar++; }
        }

        return correctSoFar;
    }

    public bool CheckIfWon(int[] pattern, int[] indices, GameObject historyPanel)
    {
        var correctSoFar = GetCorrectNumberOfItems(pattern, indices);
        print($"Correct guesses: {correctSoFar}");
        if (correctSoFar != 4) return false;
        historyPanel.GetComponent<Image>().sprite = winSprite;
        return true;
    }
}
