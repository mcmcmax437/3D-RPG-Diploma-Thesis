using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrExample_4 : MonoBehaviour
{
    public GameObject[] Cubes;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 10; i < Cubes.Length; i++)
            {
                Cubes[i].SetActive(false);
            }                 

        }

        if (Input.GetMouseButtonDown(1))
        {
            for (int i = 0; i < Cubes.Length; i++)
            {
                Cubes[i].SetActive(true);
            }

        }
    }
}
