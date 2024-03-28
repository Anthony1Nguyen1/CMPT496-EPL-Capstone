using System.Collections;
using UnityEngine;

public class ResultsAnimation : MonoBehaviour
{
    [SerializeField] private float winDelay = 10.5f;
    [SerializeField] private float lossDelay = 3.0f;
    [SerializeField] private GameObject winBoard;
    [SerializeField] private GameObject lossBoard;
    [SerializeField] private GameObject middleContent;

    public void PlayResultsAnimation(bool result) { StartCoroutine(ShowResultsMenu(result)); }

    private IEnumerator ShowResultsMenu(bool gameWon)
    {
        var delay = gameWon ? winDelay : lossDelay;
        yield return new WaitForSeconds(delay);

        middleContent.SetActive(false);

        if (gameWon) { winBoard.SetActive(true);  }
        else         { lossBoard.SetActive(true); }
    }
}
