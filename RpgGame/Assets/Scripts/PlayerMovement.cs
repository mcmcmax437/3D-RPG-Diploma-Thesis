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
    public static int ray_numbers = 6;

    // Для камеры
    CinemachineTransposer cinemachineTransposer;
    public CinemachineVirtualCamera playerCamera;
    private Vector3 mouse_pos;
    private Vector3 current_pos;




    void Start()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();

        //cinemachineTransposer = playerCamera.GetCinemachineComponent<CinemachineTransposer>();
        //current_pos = cinemachineTransposer.m_FollowOffset;

    }

    void Update()
    {
        x = nav.velocity.x;
        z = nav.velocity.z;
        velocitySpeed = new Vector2(x, z).magnitude;

        //Get mouse possition
        //mouse_pos = Input.mousePosition;
        //cinemachineTransposer.m_FollowOffset = current_pos;


        
        Ray[] rays = new Ray[ray_numbers];

        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < ray_numbers; i++)
            {
                rays[i] = Camera.main.ScreenPointToRay(Input.mousePosition);
            }

            Vector3 averageHitPoint = Vector3.zero;

            foreach (Ray ray in rays)
            {
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    averageHitPoint += hit.point;
                }
            }
            averageHitPoint /= rays.Length;
            nav.destination = averageHitPoint;
        }



        // Check if the character is moving (forward or backward)
        anim.SetBool("sprinting", velocitySpeed > 0.1f);

        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetBool("sprinting", false);
            nav.destination = transform.position;
        }

        /*
        if (Input.GetMouseButton(0))
        {
            if (mouse_pos.x != 0 || mouse_pos.y != 0)
            {
                //current_pos = new Vector3(mouse_pos.x, 0, mouse_pos.y) / 400;
                //cinemachineTransposer.m_FollowOffset = current_pos;
            }
        }
        */




    }

}