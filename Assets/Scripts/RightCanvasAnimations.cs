using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightCanvasAnimations : MonoBehaviour
{
    [SerializeField] private GameObject[] cauldrons;
    [SerializeField] private GameObject[] potions;
    [SerializeField] private GameObject[] crystals;
    [SerializeField] private GameObject[] misc;
    [SerializeField] private Image wrongAnswer;
    [SerializeField] private WrongAnswerImageManager wrongAnswerImageManager;

    public void AnimateRightAnimations(int[] indices)
    {
        StartCoroutine(Animate(indices));
    }

    private IEnumerator Animate(IReadOnlyList<int> indices)
    {
        //wrongAnswerImageManager.ResetWrongAnswerImageFlag();

        ShowSelectedCauldron(indices[0]);

        // Stop the previous animations for each category.
        StopPreviousAnimations(potions); StopPreviousAnimations(crystals); StopPreviousAnimations(misc);

        yield return StartCoroutine(AnimateItems(potions, indices[1]));  // Potions
        yield return StartCoroutine(AnimateItems(crystals, indices[2])); // Crystals
        yield return StartCoroutine(AnimateItems(misc, indices[3]));     // Misc

        //yield return new WaitForSeconds(1.0f);
        //wrongAnswerImageManager.ShowWrongAnswerImage();
    }

    private void ShowSelectedCauldron(int selectedCauldronIndex)
    {
        for (var i = 0; i < cauldrons.Length; i++)
        {
            cauldrons[i].SetActive(i == selectedCauldronIndex);
        }
    }

    private void StopPreviousAnimations(IEnumerable<GameObject> items)
    {
        foreach (var item in items)
        {
            var itemAnimation = item.GetComponent<ItemAnimation>();
            if (itemAnimation != null)
            {
                itemAnimation.StopAnimation();
            }
        }
    }

    private IEnumerator AnimateItems(GameObject[] items, int selectedIndex)
    {
        if (selectedIndex < 0 || selectedIndex >= items.Length)
        {
            yield break;
        }

        var itemAnimation = items[selectedIndex].GetComponent<ItemAnimation>();
        if (itemAnimation != null)
        {
            itemAnimation.MoveToCauldron();
            yield return new WaitForSeconds(0.7f);
        }
    }
}
