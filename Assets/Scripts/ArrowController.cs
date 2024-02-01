using UnityEngine;
using TouchScript.Gestures;

public class ArrowController : MonoBehaviour
{
    public ItemController item;
    public enum ArrowDirection { Up, Down }
    public ArrowDirection direction;

    private void OnEnable()
    {
        GetComponent<PressGesture>().Pressed += PressedHandler;
    }

    private void OnDisable()
    {
        GetComponent<PressGesture>().Pressed -= PressedHandler;
    }

    private void PressedHandler(object sender, System.EventArgs e)
    {
        // Debug.Log("Print!");
        if      (direction == ArrowDirection.Up) { item.CycleUp(); }
        else if (direction == ArrowDirection.Down) { item.CycleDown(); }
    }
}
