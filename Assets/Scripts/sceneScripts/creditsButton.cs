using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class creditsButton : MonoBehaviour
{
    [SerializeField] Button _credits;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        _credits.onClick.AddListener(loadCredits);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void loadCredits()
    {
        source.PlayOneShot(clip);
        Time.timeScale = 1f;
        sceneManager.Instance.LoadCredits();
    }
}
