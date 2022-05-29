using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void ReloadGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
