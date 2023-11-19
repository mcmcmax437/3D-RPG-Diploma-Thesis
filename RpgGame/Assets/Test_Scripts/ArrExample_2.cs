using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class ArrExample_2 : MonoBehaviour
{

    public GameObject[] Cubes;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cubes[0].SetActive(false);
            Cubes[1].SetActive(false);
            Cubes[2].SetActive(false);

        }

        if (Input.GetMouseButtonDown(1))
        {
            Cubes[0].SetActive(true);
            Cubes[1].SetActive(true);
            Cubes[2].SetActive(true);

        }

    }
}
