/* Description: Script that listens for tap gestures and triggers item cycling accordingly. */

using UnityEngine;
using TouchScript.Gestures;

public class ArrowController : MonoBehaviour
{
    public ItemController item;             // The item.
    public enum ArrowDirection { Up, Down } // Enum for the two different arrow types.
    public ArrowDirection direction;

    private void OnEnable() { GetComponent<TapGesture>().Tapped += TappedHandler; }
    private void OnDisable() { GetComponent<TapGesture>().Tapped += TappedHandler; }

    // Desc: Main function for controlling cycling behaviour. Calls upon item methods depending on the arrow direction.
    // Params: sender, e
    // Return: void
    private void TappedHandler(object sender, System.EventArgs e)
    {
        if      (direction == ArrowDirection.Up) { item.CycleUp(); }
        else if (direction == ArrowDirection.Down) { item.CycleDown(); }
    }
}
