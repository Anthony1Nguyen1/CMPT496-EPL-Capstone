using UnityEngine;
using TouchScript.Gestures;

public class ArrowController : MonoBehaviour
{
    public ItemController item;
    public enum ArrowDirection { Up, Down }
    public ArrowDirection direction;

    private void OnEnable()
    {
        GetComponent<TapGesture>().Tapped += TappedHandler;
    }

    private void OnDisable()
    {
        GetComponent<TapGesture>().Tapped += TappedHandler;
    }

    private void TappedHandler(object sender, System.EventArgs e)
    {
        if      (direction == ArrowDirection.Up) { item.CycleUp(); }
        else if (direction == ArrowDirection.Down) { item.CycleDown(); }
    }
}
