using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roof : MonoBehaviour
{

    public GameObject roof;
    public GameObject props;
    public GameObject main_camera;
    public bool pub = true;
    public bool craftsMan = false;
    public bool wizard = false;
    void Start()
    {
        roof.SetActive(true);
        props.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            roof.SetActive(false);
            props.SetActive(true);
            if (pub == true)
            {
                main_camera.GetComponent<Music>().music_status = 2;
                main_camera.GetComponent<Music>().should_play = true;
            }
            if (craftsMan == true)
            {
                main_camera.GetComponent<Music>().music_status = 4;
                main_camera.GetComponent<Music>().should_play = true;
            }
            if (wizard == true)
            {
                main_camera.GetComponent<Music>().music_status = 5;
                main_camera.GetComponent<Music>().should_play = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            roof.SetActive(true);
            props.SetActive(false);          
            main_camera.GetComponent<Music>().music_status = 1;
            main_camera.GetComponent<Music>().should_play = true;      
        }
    }
}
