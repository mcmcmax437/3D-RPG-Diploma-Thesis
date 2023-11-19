using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrExample_1 : MonoBehaviour
{

    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            cube1.SetActive(false);
            cube2.SetActive(false);
            cube3.SetActive(false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            cube1.SetActive(true);
            cube2.SetActive(true);
            cube3.SetActive(true);
        }
        
    }
}
