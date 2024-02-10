using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryController : MonoBehaviour
{
    public void Submit(GameObject[] items, int tryNumber)
    {
        var row = transform.GetChild(tryNumber); // Get the current move/try.
        for (var i = 0; i < items.Length; i++)
        {
            var item = items[i];                                             // Item to be copied.
            var rowFrame = row.GetChild(i);                                   // Container for new item
            var newItem = Instantiate(item, rowFrame, false); // Copy the item over.
            newItem.transform.localScale = Vector3.one;                               // Set its scale locally.
            newItem.SetActive(true);                                                  // Make the new item active.
        }
    }
}
