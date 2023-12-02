using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pauseMenu : MonoBehaviour
{
    public static bool gamePause;
    [SerializeField] GameObject screen;
    // Start is called before the first frame update
    void Start()
    {
        gamePause = false;
        screen.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePause = !gamePause;
            pauseGame();
        }
    }

    void pauseGame()
    {
        if (gamePause)
        {
            Time.timeScale = 0f;
            screen.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            screen.SetActive(false);
        }
    }

    public void Resume()
    {
        screen.SetActive(false);
        Time.timeScale = 1f;
        
    }
}
