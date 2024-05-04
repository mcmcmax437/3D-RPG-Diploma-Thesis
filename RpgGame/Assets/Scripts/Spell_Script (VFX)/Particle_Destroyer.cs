using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Destroyer : MonoBehaviour
{

    public float life_time_for_chest = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, life_time_for_chest);
    }

   
}
