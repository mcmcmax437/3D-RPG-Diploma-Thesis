using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Amb : MonoBehaviour
{

    private AudioSource audio_Player;
    public WaitForSeconds delay_time = new WaitForSeconds(27);

    // Start is called before the first frame update
    void Start()
    {
        audio_Player = GetComponent<AudioSource>();
        StartCoroutine(SFX_BirdSound());

    }

    IEnumerator SFX_BirdSound()
    {
        yield return delay_time;
        audio_Player.Play();
        StartCoroutine(SFX_BirdSound());
    }


}
