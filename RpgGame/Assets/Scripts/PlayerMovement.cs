using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;




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

    private bool isPlayerSelectScene;
    public static bool canMove = true;




    void Start()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();

        //cinemachineTransposer = playerCamera.GetCinemachineComponent<CinemachineTransposer>();
        //current_pos = cinemachineTransposer.m_FollowOffset;

        if (SceneManager.GetActiveScene().name == "PlayerSelect")
        {
            isPlayerSelectScene = true;
      
        }

    }

    void Update()
    {

        if (isPlayerSelectScene == false)
        {
            x = nav.velocity.x;
            z = nav.velocity.z;
            velocitySpeed = new Vector2(x, z).magnitude;

            Ray[] rays = new Ray[ray_numbers];

            if (Input.GetMouseButtonDown(0))
            {
                if (canMove == true)
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
            }



            // Check if the character is moving (forward or backward)
            anim.SetBool("sprinting", velocitySpeed > 0.1f);

            if (Input.GetKeyDown(KeyCode.S))
            {
                anim.SetBool("sprinting", false);
                nav.destination = transform.position;
            }
        }
        else
        {

        }
    }

}