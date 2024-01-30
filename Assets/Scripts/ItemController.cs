using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public GameObject[] dotPrefabs;
    private int currentIndex = 0;

    public void CycleDown()
    {
        currentIndex = (currentIndex - 1 + dotPrefabs.Length) % dotPrefabs.Length;
        UpdateDotColor();
    }

    public void CycleUp()
    {
        currentIndex = (currentIndex + 1) % dotPrefabs.Length;
        UpdateDotColor();
    }

    private void UpdateDotColor()
    {
        // foreach (Transform child in transform)
        // {
        //     Destroy(child.gameObject);
        // }

        // var newDot = Instantiate(dotPrefabs[currentIndex], transform.position, Quaternion.identity);
        // newDot.transform.parent = transform;
        print("hi");
    }
}
