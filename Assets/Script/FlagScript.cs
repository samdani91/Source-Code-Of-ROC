using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagScript : MonoBehaviour
{
    public PlayerMovement player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Hero").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            StartCoroutine(LoadNextSceneWithDelay(2f));
        }
    }

    IEnumerator LoadNextSceneWithDelay(float delay)
    {

        yield return new WaitForSeconds(delay);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
