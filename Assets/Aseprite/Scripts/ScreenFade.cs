using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    [SerializeField] private Image fadeImage; // Reference to the UI image used for fading
    [SerializeField] private float fadeDuration = 1.0f; // Duration of each fade effect
    [SerializeField] private float delayBetweenFades = 0.0f; // Delay between the fade-in and fade-out

    private void Start()
    {
        // Ensure the fade image is fully transparent at the beginning
        SetAlpha(0);
    }

    // Fade in and then fade out
    public void FadeInOut()
    {
        StartCoroutine(FadeInAndOut());
    }

    // Fade from transparent to black
    private IEnumerator FadeInAndOut()
    {
        yield return StartCoroutine(FadeToColor(Color.black)); // Fade in

        // Wait for a brief period
        yield return new WaitForSeconds(delayBetweenFades);

        yield return StartCoroutine(FadeToColor(Color.clear)); // Fade out
    }

    // Fade from current color to target color
    private IEnumerator FadeToColor(Color targetColor)
    {
        // Calculate the start and end colors based on the target color
        Color startColor = fadeImage.color;
        Color endColor = targetColor;

        // Calculate the increment for each step of the fade
        float fadeSpeed = 1.0f / fadeDuration;

        // Perform the fade
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            // Calculate the new color based on the elapsed time
            Color newColor = Color.Lerp(startColor, endColor, elapsedTime * fadeSpeed);

            // Set the color of the fade image
            fadeImage.color = newColor;

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the fade image is set to the target color
        fadeImage.color = endColor;
    }

    // Set the alpha value of the fade image
    private void SetAlpha(float alpha)
    {
        Color color = fadeImage.color;
        color.a = alpha;
        fadeImage.color = color;
    }
}