using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Settings_Script : MonoBehaviour
{
    public Slider Slider_for_Music;
    public Slider Slider_for_SFX;
    public AudioMixer Music_Mixer;
    public AudioMixer SFX_Mixer;

    public void Music_Change()
    {
        Music_Mixer.SetFloat("Music_Mixer_volume", Slider_for_Music.value);
    }
    public void SFX_Change()
    {
        SFX_Mixer.SetFloat("Sound_SFX_volume", Slider_for_SFX.value);
    }

    public void Exit_to_Main_Menu()
    {
        SceneManager.LoadScene(0);
    }
}
 