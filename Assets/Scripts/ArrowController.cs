using UnityEngine;
using TouchScript.Gestures;

public class ArrowController : MonoBehaviour
{
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
        Debug.Log("Print!");
    }
}
