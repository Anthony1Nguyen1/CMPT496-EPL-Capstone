using System.Collections;
using UnityEngine;

public class ResultsAnimation : MonoBehaviour
{
    [SerializeField] private float delayBeforeResults = 10.5f;
    [SerializeField] private GameObject winBoard;
    [SerializeField] private GameObject lossBoard;
    [SerializeField] private GameObject middleContent;

    public void PlayResultsAnimation(bool result) { StartCoroutine(ShowResultsMenu(result)); }

    private IEnumerator ShowResultsMenu(bool gameWon)
    {
        yield return new WaitForSeconds(delayBeforeResults);
        middleContent.SetActive(false);
        if (gameWon) { winBoard.SetActive(true); }
        else         { lossBoard.SetActive(true); }
    }
}
