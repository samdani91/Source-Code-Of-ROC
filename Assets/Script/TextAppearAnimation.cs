using System.Collections;
using TMPro;  
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextAppearAnimation : MonoBehaviour
{
    public TextMeshProUGUI titleText;     
    public TextMeshProUGUI descriptionText;  
    public AudioClip typingClip;              
    public float typingSpeed = 0.05f;        

    private string fullTitleText;
    private string fullDescriptionText;
    private AudioSource audioSource;          

    void Start()
    {

        fullTitleText = titleText.text;
        fullDescriptionText = descriptionText.text;

        titleText.text = "";
        descriptionText.text = "";

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = typingClip;       
        audioSource.loop = true;            
        audioSource.playOnAwake = false;    


        StartCoroutine(AnimateTextSequence());
    }

    IEnumerator AnimateTextSequence()
    {
  
        yield return StartCoroutine(ShowText(titleText, fullTitleText));
        yield return StartCoroutine(ShowText(descriptionText, fullDescriptionText));
        SceneController.instance.NextLevel();

    }

    IEnumerator ShowText(TextMeshProUGUI textComponent, string fullText)
    {
       
        if (audioSource != null && typingClip != null)
        {
            audioSource.Play();
        }


        for (int i = 0; i <= fullText.Length; i++)
        {
            textComponent.text = fullText.Substring(0, i);
            yield return new WaitForSeconds(typingSpeed);
        }


        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
