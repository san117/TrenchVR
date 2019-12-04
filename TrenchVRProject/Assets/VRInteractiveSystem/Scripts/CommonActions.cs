using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CommonActions : MonoBehaviour
{
    public void ChangueScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
