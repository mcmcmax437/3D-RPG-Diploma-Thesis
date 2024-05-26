using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonFire : MonoBehaviour
{
    public GameObject Save_Canvas;
    public GameObject saving___;
    private bool can_be_saved = true;
    private bool fire_was_created = false;

    public GameObject fire_VFX;
    public Transform position_of_VFX;
    void Start()
    {
        Save_Canvas.SetActive(false);
        saving___.SetActive(false);
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && can_be_saved == true)
        {
            Save_Canvas.SetActive(true);
            Time.timeScale = 0;
            can_be_saved = false;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && can_be_saved == false)
        {
            can_be_saved = true; 
        }
    }
    IEnumerator SaveProcess()
    {
        yield return new WaitForSeconds(1);
        Save_Canvas.SetActive(false);
        saving___.SetActive(false);
    }

    public void No()
    {
        Time.timeScale = 1;
        SaveScript.should_be_saved = false;
        Save_Canvas.SetActive(false);
    }
    public void Yes()
    {
        SaveScript.should_be_saved = true;
        saving___.SetActive(true);
        Time.timeScale = 1;
        StartCoroutine(SaveProcess());
        if(fire_was_created == false)
        {
            fire_was_created = true;
            Instantiate(fire_VFX, position_of_VFX.position, position_of_VFX.rotation);
        }
    }
}
