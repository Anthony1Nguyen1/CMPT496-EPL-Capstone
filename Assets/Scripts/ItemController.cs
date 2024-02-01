using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] public GameObject[] dotPrefabs;
    private int currentIndex = 0;

    public void CycleDown()
    {
        Debug.Log("Cycle down!");
        currentIndex = (currentIndex - 1 + dotPrefabs.Length) % dotPrefabs.Length;
        UpdateDotColor();
    }

    public void CycleUp()
    {
        Debug.Log("Cycle up!");
        currentIndex = (currentIndex + 1) % dotPrefabs.Length;
        UpdateDotColor();
    }

private void UpdateDotColor()
{
    Debug.Log("Updating dot color...");
    Debug.Log("child count: " + transform.childCount);
    foreach (Transform child in transform)
    {
        Debug.Log("Destroying child: " + child.name);
        Destroy(child.gameObject);
    }

    Debug.Log("Instantiating new dot...");
    var newDot = Instantiate(dotPrefabs[currentIndex], transform.position, Quaternion.identity);
    newDot.transform.parent = transform;
}
}
