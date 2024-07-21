using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
public class mainMenu : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup mixer;
    [SerializeField] private Slider gameSpeed, gameVolume;
    [SerializeField] private GameObject settings;
    [SerializeField] private AudioSource button;
    void Start()
    {
        settings.SetActive(false);
        gameSpeed.value = PlayerPrefs.GetFloat("Speed", 5f);
        gameVolume.value = PlayerPrefs.GetFloat("volume", 0.75f);

      
        gameVolume.onValueChanged.AddListener(val =>
        {
            //print("music value changed");
            mixer.audioMixer.SetFloat("Volume", 20 * Mathf.Log10(val + float.Epsilon));
            PlayerPrefs.SetFloat("Volume", val);

        });
    }

    
    void Update()
    {
        
    }
   public void startGame() {
        button.Play();
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        button.Play();
        Application.Quit();
    }
    public void openSettings()
    {
        button.Play();
        settings.SetActive(true);
        
    }
    public void closeSettings()
    {
        button.Play();
        settings.SetActive(false);
        
    }
    public void changeSpeed()
    {
        PlayerPrefs.SetFloat("Speed", gameSpeed.value);
    }
}
