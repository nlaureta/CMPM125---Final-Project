using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class creditsScene : MonoBehaviour
{
    [SerializeField] Button _developers;
    [SerializeField] Button _fonts;
    [SerializeField] Button _sounds;
    [SerializeField] Button _assets;
    [SerializeField] private GameObject developerCredits;
    [SerializeField] private GameObject fontsCredits;
    [SerializeField] private GameObject soundsCredits;
    [SerializeField] private GameObject assetsCredits;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        developerCredits.SetActive(true);
        fontsCredits.SetActive(false);
        soundsCredits.SetActive(false);
        assetsCredits.SetActive(false);

        //Button on click events
        _developers.onClick.AddListener(loadDeveloperCredits);
        _fonts.onClick.AddListener(loadFontsCredits);
        _sounds.onClick.AddListener(loadSoundsCredits);
        _assets.onClick.AddListener(loadAssetsCredits);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void loadDeveloperCredits()
    {
        developerCredits.SetActive(true);
        fontsCredits.SetActive(false);
        soundsCredits.SetActive(false);
        assetsCredits.SetActive(false);
        source.PlayOneShot(clip);
    }

    private void loadFontsCredits()
    {
        developerCredits.SetActive(false);
        fontsCredits.SetActive(true);
        soundsCredits.SetActive(false);
        assetsCredits.SetActive(false);
        source.PlayOneShot(clip);
    }

    private void loadSoundsCredits()
    {
        developerCredits.SetActive(false);
        fontsCredits.SetActive(false);
        soundsCredits.SetActive(true);
        assetsCredits.SetActive(false);
        source.PlayOneShot(clip);
    }

    private void loadAssetsCredits()
    {
        developerCredits.SetActive(false);
        fontsCredits.SetActive(false);
        soundsCredits.SetActive(false);
        assetsCredits.SetActive(true);
        source.PlayOneShot(clip);
    }
}
