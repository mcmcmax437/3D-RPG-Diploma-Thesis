using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;



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
        //Подсчет скорости 
        x = nav.velocity.x;
        z = nav.velocity.z;
        velocitySpeed = x + z;

        //Получаем позицию курсора
        pos = Input.mousePosition;
        cinemTransposer.m_FollowOffset = currPos;

        if(Input.GetMouseButton(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                nav.destination = hit.point;
            }
        }
        if(velocitySpeed != 0)
        {
            anim.SetBool("sprinting", true);
        }
        if (velocitySpeed == 0)
        {
            anim.SetBool("sprinting", false);
        }

        if(Input.GetMouseButton(1))
        {
            if(pos.x != 0 || pos.y != 0)
            {
                currPos = pos / 200;
            }
        }
      
    }
}
