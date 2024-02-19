/* Description: Script that listens for tap gestures and triggers item cycling accordingly. */

using UnityEngine;
using TouchScript.Gestures;

public class ArrowController : MonoBehaviour
{
    [SerializeField] public ItemController itemController;
    [SerializeField] private SubmitController submitController;
    [SerializeField] public ArrowDirection direction;
    public enum ArrowDirection { Upwards, Downwards } // Enum for the two different arrow types.

    private void OnEnable() { GetComponent<TapGesture>().Tapped += TappedHandler; }
    private void OnDisable() { GetComponent<TapGesture>().Tapped -= TappedHandler; }

    // Purpose: Main function for controlling cycling behaviour. Calls upon item methods depending on the arrow direction.
    // Params: sender, e
    // Return: void
    private void TappedHandler(object sender, System.EventArgs e)
    {
        if (submitController.gameWon == false)
        {
            if      (direction == ArrowDirection.Upwards) { itemController.CycleUp(); }
            else if (direction == ArrowDirection.Downwards) { itemController.CycleDown(); }
        }
    }
}
