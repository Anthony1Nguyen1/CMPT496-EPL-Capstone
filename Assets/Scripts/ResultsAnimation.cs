using System.Collections;
using UnityEngine;

public class ResultsAnimation : MonoBehaviour
{
    [SerializeField] private float delayBeforeResults = 10.5f;
    [SerializeField] private GameObject Board;
    [SerializeField] private GameObject MiddleContent;

    public void PlayResultsAnimation() { StartCoroutine(ShowResultsMenuAfterDelay()); }

    private IEnumerator ShowResultsMenuAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeResults);

        MiddleContent.SetActive(false);
        Board.SetActive(true);
    }
}
