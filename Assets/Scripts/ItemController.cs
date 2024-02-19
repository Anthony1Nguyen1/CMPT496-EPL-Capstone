/* Description: Script that handles the main category/item pane in the middle. */

using UnityEngine;

public class ItemController : MonoBehaviour
{
    public int CurrentIndex { get; private set; } = 0;

    // Purpose: Instantiates all of the items and adds them to the dot pool for that item.
    // Params: none
    // Return: void
    private void Start()
    {
        // Populate the itemPool with the child GameObjects of the Frame object
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    // Purpose: Cycles through the dotPool, downwards.
    // Params: none
    // Return: void
    public void CycleDown()
    {
        ToggleActive(false);
        CurrentIndex = (CurrentIndex - 1 + transform.childCount) % transform.childCount;
        ToggleActive(true);
    }

    // Purpose: Cycles through the dotPool, upwards.
    // Params: none
    // Return: void
    public void CycleUp()
    {
        ToggleActive(false);
        CurrentIndex = (CurrentIndex + 1) % transform.childCount;
        ToggleActive(true);
    }

    // Purpose: Deactivates or activates a prefab.
    // Params: isActive: a boolean for whether the prefab is currently enabled/disabled.
    // Return: void
    private void ToggleActive(bool isActive)
    {
        transform.GetChild(CurrentIndex).gameObject.SetActive(isActive);
    }

    // Purpose: Deactivates the item as well as its candidates.
    // Params: none
    // Return: void
    public void DeactivateDots()
    {
        CurrentIndex = 0;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

}
