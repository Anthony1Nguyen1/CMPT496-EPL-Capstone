using UnityEngine;
using System.Collections;

public class ItemAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator; // Reference to the Animator component

    // Play the move animation for the item
    public void MoveToCauldron()
    {
        if (animator != null)
        {
            StartCoroutine(PlayAnimationWithDelay());
        }
    }

    private IEnumerator PlayAnimationWithDelay()
    {

        yield return new WaitForSeconds(1.0f); // Adjust the delay as needed

        animator.SetTrigger("MoveTrigger"); // Set the trigger or boolean parameter to start the animation
    }
}