using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playButtonScript : MonoBehaviour
{
    [SerializeField] Button _playButton;
    [SerializeField] AudioSource source;
    [SerializeField] private AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        _playButton.onClick.AddListener(loadGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void loadGame()
    {
        Time.timeScale = 1f;
        sceneManager.Instance.LoadGameScreen();
        source.PlayOneShot(clip);

    }
}
