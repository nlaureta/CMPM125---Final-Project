using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainMenu : MonoBehaviour
{
    [SerializeField] Button _mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        _mainMenu.onClick.AddListener(loadMenu);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void loadMenu()
    {
        Time.timeScale = 1f;
        sceneManager.Instance.LoadMainMenu();
    }
}