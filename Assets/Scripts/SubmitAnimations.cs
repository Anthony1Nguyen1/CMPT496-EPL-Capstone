using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitAnimations : MonoBehaviour
{
    [SerializeField] private Animator animator = null;

    public void PlayAnimations(int TryNumber)
    {
        animator.Play("Move0");
    }
}
