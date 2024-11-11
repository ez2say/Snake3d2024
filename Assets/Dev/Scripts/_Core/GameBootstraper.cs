using Root.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum IDScene { INIT = 0, GAMEPLAY = 1 }

public class GameBootstraper : MonoBehaviour
{
    private void Awake()
    {
        int indexScene = SceneManager.GetActiveScene().buildIndex;
        
        if (!GameCore.IsLoad && indexScene != (int) IDScene.INIT)
        {
            SceneManager.LoadScene((int)IDScene.INIT);
        }

    }
}