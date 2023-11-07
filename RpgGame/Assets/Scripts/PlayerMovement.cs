using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.AI;



public class PlayerMovement : MonoBehaviour
{

    // Для движения персонажа {1}
    private UnityEngine.AI.NavMeshAgent nav;
    private Animator anim;
    private Ray ray;
    private RaycastHit hit;

    private float x;
    private float z;
    private float velocitySpeed;

    // Для камеры
    CinemachineTransposer cinemTransposer;
    public CinemachineVirtualCamera playerCamera;
    private Vector3 pos;
    private Vector3 currPos;

    

    
    void Start()
    { 
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();

        cinemTransposer = playerCamera.GetCinemachineComponent<CinemachineTransposer>();
        currPos = cinemTransposer.m_FollowOffset;

    }

    void Update()
    {
        x = nav.velocity.x;
        z = nav.velocity.z;
        velocitySpeed = new Vector2(x, z).magnitude;

        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                nav.destination = hit.point;
            }
        }

        // Check if the character is moving (forward or backward)
        anim.SetBool("sprinting", velocitySpeed > 0.1f);
    }

}
