using UnityEngine;
using UnityEngine.UI;

public class ResultsController : MonoBehaviour
{
    // Pattern and items
    [SerializeField] private int[] pattern;
    [SerializeField] private GameObject[] winItems;
    [SerializeField] private GameObject[] loseItems;

    // Sprites to be indexed into
    [SerializeField] public Sprite[] cauldrons;
    [SerializeField] public Sprite[] potions;
    [SerializeField] public Sprite[] crystals;
    [SerializeField] public Sprite[] misc;

    public void SetPattern(int[] patternIndices) { pattern = patternIndices; }

    public void FillFrames()
    {
        for (var i = 0; i < winItems.Length; i++)
        {
            var winImageComponent = winItems[i].GetComponent<Image>();
            var loseImageComponent = loseItems[i].GetComponent<Image>();

            // Switch case to determine which category we're currently in.
            Sprite[] categorySprites;
            switch (i)
            {
                case 0: categorySprites = cauldrons; break;
                case 1: categorySprites = potions;   break;
                case 2: categorySprites = crystals;  break;
                case 3: categorySprites = misc;      break;
                default: return;
            }

            winImageComponent.sprite  = categorySprites[pattern[i]];
            loseImageComponent.sprite = categorySprites[pattern[i]];
        }
    }
}
