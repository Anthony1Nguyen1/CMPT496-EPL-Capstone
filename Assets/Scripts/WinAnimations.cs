using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAnimations : MonoBehaviour
{
    [SerializeField] private Animator animator = null;

    public void PlayAnimations()
    {
        Debug.Log("WON ANIMATION");
        animator.SetTrigger("GameWon");
    }
}
