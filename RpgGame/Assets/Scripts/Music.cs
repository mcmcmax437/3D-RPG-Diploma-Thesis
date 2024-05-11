using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    private AudioSource audio_Player;
    public AudioClip main_thema1;
    public AudioClip main_thema2;
    public AudioClip main_thema3;

    public AudioClip shop_thema;
    public AudioClip battle_mode_thema;
    public AudioClip craftsMan_thema;
    public AudioClip wizzard_thema;

    public int music_status = 1;  //1 = main thema
    [HideInInspector]
    public bool should_play = true; 


    // Start is called before the first frame update
    void Start()
    {
        audio_Player = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (should_play == true)
        {
            should_play = false;
            if (music_status == 1)
            {
                audio_Player.clip = main_thema1;
                audio_Player.Play();
            }
            if (music_status == 2)
            {
                audio_Player.clip = shop_thema;
                audio_Player.Play();
            }
            if (music_status == 3)
            {
                audio_Player.clip = battle_mode_thema;
                audio_Player.Play();
            }
            if (music_status == 4)
            {
                audio_Player.clip = craftsMan_thema;
                audio_Player.Play();
            }
            if (music_status == 5)
            {
                audio_Player.clip = wizzard_thema;
                audio_Player.Play();
            }
        }  
    }
}
