using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Point : MonoBehaviour
{

    public float speed = 1.0f;
    public bool should_rotate = false;
    public bool move_to_target = true;
    

    // Update is called once per frame
    void Update()
    {
        if (should_rotate == true)
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }
        if(move_to_target == true)
        { 
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
