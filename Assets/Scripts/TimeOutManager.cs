using UnityEngine;
using UnityEngine.SceneManagement;
using TouchScript.Gestures;

public class TimeoutManager : MonoBehaviour
{
    [SerializeField] private float timeoutDuration = 60f; // Timeout duration in seconds
    private float timer = 0f;

    private void OnEnable()
    {
        // Subscribe to touch events
        GetComponent<TapGesture>().Tapped += OnTapped;
    }

    private void OnDisable()
    {
        // Unsubscribe from touch events
        GetComponent<TapGesture>().Tapped -= OnTapped;
    }

    private void OnTapped(object sender, System.EventArgs e)
    {
        // Reset the timer on tap
        timer = 0f;
    }

    private void Update()
    {
        // Increment the timer if no touch occurs
        if (Input.touchCount == 0)
        {
            timer += Time.deltaTime;

            // Check if the timeout duration is reached
            if (timer >= timeoutDuration)
            {
                // Load the menu scene
                SceneManager.LoadScene("WizardsAssistance");
            }
        }
        else
        {
            // Reset the timer if touch occurs
            timer = 0f;
        }
    }
}