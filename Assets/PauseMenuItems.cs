using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuItems : MonoBehaviour
{
    [SerializeField] bool sfx = false;
    Slider slider;
    private void Start()
    {
        if(GetComponent<Slider>() != null)
        {
            slider = GetComponent<Slider>();
            if(sfx)
            {
                slider.value = PlayerPrefs.GetFloat("SFXVolume", 1);
            }
            else
            {
                slider.value = PlayerPrefs.GetFloat("MusicVolume", 1);

            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ChangeSfx()
    {
        PlayerPrefs.SetFloat("SFXVolume", slider.value);
    }

    public void ChangeMusic()
    {
        PlayerPrefs.SetFloat("MusicVolume", slider.value);
        GameObject.Find("Music").GetComponent<AudioSource>().volume = slider.value;
    }
}
