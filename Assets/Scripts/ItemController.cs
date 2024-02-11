/* Description: Script that handles the main category/item pane in the middle. */

using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] public GameObject[] dotPrefabs; // These are the display items (entered on Unity-side).
    public List<GameObject> dotPool;                 // Script-side list of the items.
    public int currentIndex = 0;                     // Index of which move the player is currently on.
    public bool itemChosen = false;                  // Set to true when an arrow is clicked (helpful for submission script)

    // Desc: Instantiates all of the items and adds them to the dot pool for that item.
    // Params: none
    // Return: void
    private void Start()
    {
        dotPool = new List<GameObject>();
        foreach (var item in dotPrefabs)
        {
            var dot = Instantiate(item, transform.position, Quaternion.identity);
            dot.transform.parent = transform;
            dot.SetActive(false);
            dotPool.Add(dot);
        }
    }

    // Desc: Cycles through the dotPool, downwards.
    // Params: none
    // Return: void
    public void CycleDown()
    {
        itemChosen = true;
        dotPool[currentIndex].SetActive(false);
        currentIndex = (currentIndex - 1 + dotPool.Count) % dotPool.Count;
        dotPool[currentIndex].SetActive(true);
    }

    // Desc: Cycles through the dotPool, upwards.
    // Params: none
    // Return: void
    public void CycleUp()
    {
        itemChosen = true;
        dotPool[currentIndex].SetActive(false);
        currentIndex = (currentIndex + 1) % dotPool.Count;
        dotPool[currentIndex].SetActive(true);
    }

    // Desc: Deactivates the item as well as its candidates.
    // Params: none
    // Return: void
    public void DeactivateDots()
    {
        itemChosen = false;
        foreach (var dot in dotPool)
        {
            dot.SetActive(false);
        }
    }

}
