using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public static sceneManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public enum Scene
    {
        mainMenu,
        gameScreen
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(Scene.mainMenu.ToString());
    }

    public void LoadGameScreen()
    {
        SceneManager.LoadScene(Scene.gameScreen.ToString());
    }
}
