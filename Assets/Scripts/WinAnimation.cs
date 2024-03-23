using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinAnimation : MonoBehaviour
{
    [SerializeField] private ParticleSystem smoke;
    [SerializeField] private GameObject wizard;

    public void AnimateWinAnimations()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        yield return new WaitForSeconds(1.0f);
        smoke.Play();
        yield return new WaitForSeconds(4.0f);
        wizard.SetActive(true);
    }
}
