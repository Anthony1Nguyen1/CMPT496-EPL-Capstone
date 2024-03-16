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
            // Stop the current animation
            StopAnimation();

            // Start the new animation
            animator.SetTrigger("MoveTrigger");
        }
    }

    // Method to stop the animation
    public void StopAnimation()
    {
        if (animator != null)
        {
            animator.Rebind(); // Reset the animator to its initial state
        }
    }
}