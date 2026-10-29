using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    // gets the slider
    [SerializeField]
    Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        // checks for save data 
        // if no set to 50% 
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 0.5f);
        }
        else 
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        // makes the slider value the game volume prsentage 
        // slider is 0-1 so 0.5 = game sound 50 % 
        AudioListener.volume = volumeSlider.value; 
        Save();
    }

    private void Load()
    {
        //get number stored in musicVolume
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        // saves number under musicVolume
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }


}
