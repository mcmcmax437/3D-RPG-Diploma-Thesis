using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Goblin_Movement : MonoBehaviour
{
    public NavMeshAgent nav;
    public float waitingTime = 4f;
    public float rotationTime = 2f;
    public float speed_walking = 6f;
    public float speed_running = 9f;

    public float view_range = 15f;
    public float view_angle = 90f;
    public LayerMask player_Mask;
    public LayerMask obstacles_Mask;
    public float mesh_Resolution = 1f;
    public int edge_Iteretiopns = 4;
    public float edge_Distance = 0.5f;

    public Transform[] patroling_points;
    public int m_Current_Patroling_Points;

    Vector3 player_last_place = Vector3.zero;
    Vector3 m_player_position;

    float m_WaitTime;
    float m_TimeToRotate;
    bool m_PlayerInRahge;
    bool m_Player_Near;
    bool m_is_patrolling;
    bool m_caught_Player;
    
    void Start()
    {
       /* m_player_position = Vector3.zero;
        m_is_patrolling = true;
        m_caught_Player = false;
        m_PlayerInRahge = false;
        m_WaitTime = waitingTime;
        m_TimeToRotate = rotationTime;

        m_Current_Patroling_Points = 0;
        nav = GetComponent<NavMeshAgent>();

        nav.isStopped = false;
        nav.speed = speed_walking;
        nav.SetDestination(patroling_points[m_Current_Patroling_Points].position);*/
    }

    
    void Update()
    {
        
    }

   /* void CaughtPlayer()
    {
        m_caught_Player = true;
    }

    void LookingPlayer(Vector3 player)
    {
        nav.SetDestination(player);
        if(Vector3.Distance(transform.position, player) <= 0.3)
        {
            if (m_WaitTime <= 0)
            {
                m_Player_Near = false;
                Move(speed_walking);
                nav.SetDestination(patroling_points[m_Current_Patroling_Points].position);
                m_WaitTime = waitingTime;
                m_TimeToRotate = rotationTime;
            }
            else
            {

            }
        }
    }

    void Move(float speed)
    {
        nav.isStopped = false;
        nav.speed = speed;
    }

    void Stop()
    {
        nav.isStopped = true;
        nav.speed = 0;
    }

    public void NextPoint()
    {
        m_Current_Patroling_Points = (m_Current_Patroling_Points + 1) % patroling_points.Length;
        nav.SetDestination(patroling_points[m_Current_Patroling_Points].position);
    }

    void EnvironmentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, view_range, player_Mask);

        for(int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
        }
    } */
}
