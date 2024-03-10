using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    public int CurrentIndex { get; set; }
    public GameObject initObject;
    public Sprite[] sprites;

    [SerializeField] private bool shouldReset;

    // Purpose: Cycles through the images downwards.
    // Params: none
    // Return: void
    public void CycleDown()
    {
        ToggleActive(false);
        CurrentIndex = (CurrentIndex - 1 + sprites.Length) % sprites.Length;
        ToggleActive(true);
    }

    // Purpose: Cycles through the images upwards.
    // Params: none
    // Return: void
    public void CycleUp()
    {
        ToggleActive(false);
        CurrentIndex = (CurrentIndex + 1) % sprites.Length;
        ToggleActive(true);
    }

    // Purpose: Deactivates or activates an image.
    // Params: isActive: a boolean for whether the image is currently enabled/disabled.
    // Return: void
    protected virtual void ToggleActive(bool isActive)
    {
        var image = initObject.transform;
        if (isActive)
        {
            image.GetComponent<Image>().sprite = sprites[CurrentIndex];
        }
        image.gameObject.SetActive(isActive);
        initObject.SetActive(isActive); // Hide the invisible object when an arrow is clicked
    }

    // Purpose: Deactivates all images.
    // Params: none
    // Return: void
    public virtual void DeactivateDots()
    {
        CurrentIndex = 0;
        initObject.SetActive(false);
    }
}
