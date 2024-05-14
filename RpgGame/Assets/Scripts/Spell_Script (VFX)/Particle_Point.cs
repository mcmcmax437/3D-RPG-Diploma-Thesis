using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Point : MonoBehaviour
{
    public int damage = 30;
    public float speed = 1.0f;
    public bool should_rotate = false;
    public bool move_to_target = true;

    public GameObject object_triggered;
    

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


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy") && other.transform.gameObject != object_triggered)
        {
            other.transform.gameObject.GetComponent<EnemyMovement>().full_HP -= damage;
            object_triggered = other.transform.gameObject;
        }
        
    }
}
