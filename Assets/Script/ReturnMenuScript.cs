using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReturnMenuScript : MonoBehaviour
{
    public void ReturnToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
