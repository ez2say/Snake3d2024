using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum IDScene { INIT = 0, GAMEPLAY = 1 }

public class GameBootstraper : MonoBehaviour
{
    public static bool IsLoad = false;

    private void Awake()
    {
        int indexScene = SceneManager.GetActiveScene().buildIndex;

        if (indexScene == (int)IDScene.INIT)
        {
            IsLoad = true;
            return;
        }

        if (indexScene == (int)IDScene.GAMEPLAY && !IsLoad)
        {
            SceneManager.LoadScene((int)IDScene.INIT);

            IsLoad = true;

            return;
        }

        
    }
}