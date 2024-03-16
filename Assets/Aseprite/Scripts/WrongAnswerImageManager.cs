using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WrongAnswerImageManager : MonoBehaviour
{
    [SerializeField] private Image wrongAnswerImage;
    private bool wrongAnswerShown = false;

    // Show the wrong answer image
    public void ShowWrongAnswerImage()
    {
        if (!wrongAnswerShown)
        {
            wrongAnswerImage.gameObject.SetActive(true);
            wrongAnswerShown = true;
            // Start a coroutine to hide the wrong answer image after a delay
            StartCoroutine(HideWrongAnswerImageAfterDelay());
        }
    }

    // Hide the wrong answer image
    private IEnumerator HideWrongAnswerImageAfterDelay()
    {
        yield return new WaitForSeconds(1.0f); // Adjust as needed
        HideWrongAnswerImage();
    }

    // Hide the wrong answer image
    public void HideWrongAnswerImage()
    {
        wrongAnswerImage.gameObject.SetActive(false);
        wrongAnswerShown = false;
    }

    // Reset the flag to show the wrong answer image again
    public void ResetWrongAnswerImageFlag()
    {
        wrongAnswerShown = false;
    }
}
