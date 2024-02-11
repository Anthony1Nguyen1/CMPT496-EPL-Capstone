/* Description: Script that handles the main category/item pane in the middle. */

using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public List<GameObject> itemPool; // List of the items.
    public int currentIndex = 0;      // Index of which move the player is currently on.
    public bool itemChosen = false;   // Set to true when an arrow is clicked (helpful for submission script)

    // Desc: Instantiates all of the items and adds them to the dot pool for that item.
    // Params: none
    // Return: void
    private void Start()
    {
        // Populate the itemPool with the child GameObjects of the Frame object
        foreach (Transform child in transform)
        {
            itemPool.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
    }

    // Desc: Cycles through the dotPool, downwards.
    // Params: none
    // Return: void
    public void CycleDown()
    {
        itemChosen = true;
        itemPool[currentIndex].SetActive(false);
        currentIndex = (currentIndex - 1 + itemPool.Count) % itemPool.Count;
        itemPool[currentIndex].SetActive(true);
    }

    // Desc: Cycles through the dotPool, upwards.
    // Params: none
    // Return: void
    public void CycleUp()
    {
        itemChosen = true;
        itemPool[currentIndex].SetActive(false);
        currentIndex = (currentIndex + 1) % itemPool.Count;
        itemPool[currentIndex].SetActive(true);
    }

    // Desc: Deactivates the item as well as its candidates.
    // Params: none
    // Return: void
    public void DeactivateDots()
    {
        itemChosen = false;
        currentIndex = 0;
        foreach (var item in itemPool)
        {
            item.SetActive(false);
        }
    }

}
