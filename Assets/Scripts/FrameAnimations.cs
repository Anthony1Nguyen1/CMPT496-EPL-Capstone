using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameAnimations : MonoBehaviour
{
    [SerializeField] private Animator animator = null;

    public void PlayAnimations()
    {
        animator.SetTrigger("GameWon");
    }
}
