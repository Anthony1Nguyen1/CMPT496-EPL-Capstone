using UnityEngine;
using UnityEngine.UI;

public class AnimItemController : ItemController
{
    [SerializeField] private GameObject initItem; // The GameObject to copy the sprite from

    protected override void ToggleActive(bool isActive)
    {
        base.ToggleActive(isActive); // Call the base method

        if (isActive)
        {
            // If the item is active, copy the sprite from initItem
            GetComponent<Image>().sprite = initItem.GetComponent<Image>().sprite;
        }
    }

    public override void DeactivateDots()
    {
        // You can add code here if you want to do something when DeactivateDots is called
    }
}
