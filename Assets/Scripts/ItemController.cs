using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] public GameObject[] dotPrefabs; // These are entered on Unity-side.
    private List<GameObject> dotPool;                // These are script-side.
    private int currentIndex = 0;                    // Keeps an index of what item we should be on.

    private void Start()
    {
        dotPool = new List<GameObject>();

        // Instantiate all of the dots, setting them to false.
        foreach (var item in dotPrefabs)
        {
            var dot = Instantiate(item, transform.position, Quaternion.identity);
            dot.transform.parent = transform;
            dot.SetActive(false);
            dotPool.Add(dot);
        }
    }

    public void CycleDown()
    {
        dotPool[currentIndex].SetActive(false);
        currentIndex = (currentIndex - 1 + dotPool.Count) % dotPool.Count;
        dotPool[currentIndex].SetActive(true);
    }

    public void CycleUp()
    {
        dotPool[currentIndex].SetActive(false);
        currentIndex = (currentIndex + 1) % dotPool.Count;
        dotPool[currentIndex].SetActive(true);
    }

}
