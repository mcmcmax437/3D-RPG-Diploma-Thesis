using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrExample_3 : MonoBehaviour
{
    public GameObject[] Cubes;
    private int index = 0;
    public int max = 3;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(index < max)
            {
                Cubes[index].SetActive(false); ;
                index++;
                Debug.Log("Index = " + index);
            }
           
        }

        if (Input.GetMouseButtonDown(1))
        {         
            if (index == 0)
            {
                if (index < max && index >= 0)
                {
                    Cubes[index].SetActive(true); ;
                    Debug.Log("Index = " + index);
                }
            }else
            {
                index--;
                if (index < max && index >= 0)
                {
                    Cubes[index].SetActive(true); ;
                    Debug.Log("Index = " + index);
                }
            }
        }

    }
}
