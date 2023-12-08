using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public static sceneManager Instance;
    private Animator cameraAnim;
    private GameObject mainUI;
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
        gameScreen,
        creditsScene
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(Scene.mainMenu.ToString());
        
    }

    public void LoadGameScreen()
    {
        mainUI = GameObject.Find("Canvas");
        cameraAnim = GameObject.Find("Main Camera").GetComponent<Animator>();
        mainUI.SetActive(false);
        StartCoroutine(PlayAnim());
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(Scene.creditsScene.ToString());
    }

    IEnumerator PlayAnim()
    {
        cameraAnim.Play("New Animation");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(Scene.gameScreen.ToString());
    }
}
