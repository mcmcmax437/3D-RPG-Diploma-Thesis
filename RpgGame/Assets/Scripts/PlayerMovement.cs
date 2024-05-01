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
    public static bool isPlayerMoving = false;

    public GameObject camera_1_free;
    public GameObject camera_2_static;
    private bool is_camera1_active = true;


    //for roof box colider
    public LayerMask boxLayer;

    public GameObject vfx_spawm_point;
    private WaitForSeconds nearEnemy = new WaitForSeconds(0.22f);

    public GameObject[] player_mesh_parts;


    void Start()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();

        camera_1_free.SetActive(true);
        camera_2_static.SetActive(false);
        SaveScript.vfx_spawn_point = vfx_spawm_point;
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

                        if (Physics.Raycast(ray, out hit, 300, boxLayer))
                        {
                            if (hit.transform.gameObject.CompareTag("enemy"))
                            {
                                nav.isStopped = false;
                                SaveScript.spell_target = hit.transform.gameObject;
                                averageHitPoint += hit.point;
                                StartCoroutine(MoveTo()); //wait 3 sec and than isStopped == true

                            }
                            else
                            {
                                SaveScript.spell_target = null;
                                averageHitPoint += hit.point;
                                nav.isStopped = false;
                            }                          
                        }
                    }
                    averageHitPoint /= rays.Length;
                    nav.destination = averageHitPoint;
                }
            }



            // Check if the character is moving (forward or backward)
            anim.SetBool("sprinting", velocitySpeed > 0.1f);
            if(velocitySpeed != 0)
            {
                isPlayerMoving = true;
            } 
            if (velocitySpeed == 0)
            {
                isPlayerMoving = false;
            }


            if (Input.GetKeyDown(KeyCode.S))
            {
                anim.SetBool("sprinting", false);
                nav.destination = transform.position;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if(is_camera1_active == true)
            {
                camera_1_free.SetActive(false);
                camera_2_static.SetActive(true);

                is_camera1_active = false;
            }
            else if (is_camera1_active == false)
            {
                camera_1_free.SetActive(true);
                camera_2_static.SetActive(false);

                is_camera1_active = true;
            }
        }

        //make player invisible 
        if (player_mesh_parts[0].activeSelf == true)
        {
            if(SaveScript.is_invisible == true)
            {
                for(int i = 0; i < player_mesh_parts.Length; i++)
                {
                    player_mesh_parts[i].SetActive(false);
                }
            }
        }
        //make player visible 
        if (player_mesh_parts[0].activeSelf == false)
        {
            if (SaveScript.is_invisible == false)
            {
                for (int i = 0; i < player_mesh_parts.Length; i++)
                {
                    player_mesh_parts[i].SetActive(true);
                }
            }
        }


    }


    IEnumerator MoveTo()
    {
        yield return nearEnemy;
        nav.isStopped = true;
    }
}