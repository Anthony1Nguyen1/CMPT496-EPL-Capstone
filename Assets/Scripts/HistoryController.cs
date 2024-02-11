/* Description: Script that controls the history panel. */

using System.Collections.Generic;
using UnityEngine;

public class HistoryController : MonoBehaviour
{
    // Desc: Fills a row/move in the history panel with whatever the user submitted.
    // Params:
    //     1) items: array of user-selected items
    //     2) tryNumber: the number of tries the user has attempted so far.
    // Return: void
    public void Submit(List<GameObject> items, int tryNumber)
    {
        var row = transform.GetChild(tryNumber); // Get the corresponding row in the history panel.
        for (var i = 0; i < items.Count; i++)
        {
            var item                     = items[i];                                            // Item to be copied.
            var rowFrame                 = row.GetChild(i);                                     // Square box for the new item
            var newItem                  = Instantiate(item, rowFrame, false); // Copy the item over.
            newItem.SetActive(true);                                                           // Make the new item active.
        }
    }
}
