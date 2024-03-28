using UnityEngine;

public class ResultsController : MonoBehaviour
{
    [SerializeField] private HistoryController historyController;
    [SerializeField] private int[] pattern;

    [SerializeField] public Sprite[] cauldrons;
    [SerializeField] public Sprite[] potions;
    [SerializeField] public Sprite[] crystals;
    [SerializeField] public Sprite[] misc;

    public void SetPattern(int[] patternIndices) { pattern = patternIndices; }

}
