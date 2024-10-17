using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearGasScript : MonoBehaviour
{

    Animator anim;
    AudioSource audioSource;
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }


    public void gasOpen()
    {
        if (audioSource != null)
        {

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }


            StartCoroutine(PlayAnimationWithDelay(1f));
        }
    }

    private IEnumerator PlayAnimationWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        anim.SetTrigger("tearGas");
    }

}
