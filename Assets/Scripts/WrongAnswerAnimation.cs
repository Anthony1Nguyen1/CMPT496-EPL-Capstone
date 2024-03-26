using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WrongAnswerAnimation : MonoBehaviour
{
    [SerializeField] private GameObject wrongAnswer;

    public void AnimateWrongAnswerAnimations()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        StopWrongAnswerAnimation();
        yield return new WaitForSeconds(2.5f);
        // Play the animation for the wrong answer
        PlayWrongAnswerAnimation();

        yield return null;
    }

    private void StopWrongAnswerAnimation()
    {
        var wrongAnswerAnimation = wrongAnswer.GetComponent<ItemAnimation>();
        if (wrongAnswerAnimation != null)
        {
            wrongAnswerAnimation.StopAnimation();
        }
    }

    private void PlayWrongAnswerAnimation()
    {
        var wrongAnswerAnimation = wrongAnswer.GetComponent<ItemAnimation>();
        if (wrongAnswerAnimation != null)
        {
            wrongAnswerAnimation.MoveToCauldron();
        }
    }
}
