using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    public bool Goblin_Warrior = false;
    public bool Piglins = false;
    public bool Skeleton = false;
    public bool temp_Priority = false; // temp to check
    private bool can_call_support = false;
    private Vector3 buffed_Skeleton = new Vector3(15.0f, 15.0f, 15.0f);
    private int buffed_probability = 10;
    private bool sup_skill_used = false;
    private bool change_position = false;
    private float sup_skill_CD = 10f;
    private int amount_of_reinforcment = 2;
    public GameObject support_enemy;


    public GameObject Loot_from_Enemy;

    public GameObject current_enemy;
    private bool is_outliner_active = false;

    private AnimatorStateInfo enemy_information;
    private NavMeshAgent nav;
    private Animator anim;
    private float x;
    private float z;
    private float velocitySpeed;
    public GameObject player;
    private float distance_to_player;
    private bool is_attacking;
    public float attack_Range = 2.0f;
    public float chasing_Range = 12.0f;   //range in which enemy will run after character
    public float rotation_speed = 500.0f; //perfect
    private float stop_distance = 2f;
    private float group_brain_radius = 10f;

    public Transform patrol_main_obj;
    public float patrol_radius = 15.0f;
    public float wait_time_at_point = 2.0f;

    private Vector3 targetPoint;
    private bool is_waiting;
    private float wait_timer;
    private bool is_patroling = true;

    private int maxHP;

    public int full_HP = 100;
    private int curr_HP;

    private int fear_lvl = 100;
    private int fear_lvl_curr;

    private bool enemy_is_alive = true;

    private AudioSource audio_Player;
    public AudioClip[] get_Hit_SFX;

    public GameObject bar_Container;
    public Image HP_bar;
    private float fillHealth;
    public GameObject main_camera;

    private bool destination_run = false;

    private Vector3 escape_point;
    public Transform[] escape_target_point;

    private bool roll_out = false;
    private bool roll_is_active = false;
    public float dodgeDistance = 5f;
    public float aggression_lvl = 0.5f; // 0 (passive) to 1 (aggressive)
    private bool playerNearby = false;
    private float aggression_increase = 0.05f;
    private float aggression_decrease = 0.025f;
    public float max_aggression = 1.0f;
    public float min_aggression = 0.0f;

    public bool piglin_was_hit = false;
    private bool player_is_armorless = true;
    private bool should_reset_armor_trigger = true;

    public float distance_of_ray = 12f;
    public float time_for_search = 3f;
    private Vector3 last_seen_position;
    private float search_Timer;
    private bool player_is_inSight;
    private bool look_for_player;

    // Start is called before the first frame update
    void Start()
    {
        audio_Player = GetComponent<AudioSource>();
        current_enemy.GetComponent<Outline>().enabled = false;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        nav.avoidancePriority = UnityEngine.Random.Range(5, 75);
        curr_HP = full_HP;
        maxHP = full_HP;

        if (Goblin_Warrior == true)
        {
            Set_Petrol_Destination();
        }
        if (Goblin_Warrior == true && patrol_main_obj == null)
        {
            is_patroling = false;
        }
        if (GetComponent<Enemy_Type>().enemyType == Enemy_Type.EnemyType.Piglin)
        {
            chasing_Range = 0;
        }

        if (Skeleton == true)
        {
            int random = UnityEngine.Random.Range(1, 101);
            if (random <= buffed_probability || temp_Priority == true) //10 per cent to be able to call support
            {
                can_call_support = true;
                transform.localScale = buffed_Skeleton;
            }
            else
            {
                can_call_support = false;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (main_camera == null)
        {
            main_camera = GameObject.Find("Main Camera");
        }
        if (patrol_main_obj == null)
        {
            new WaitForSeconds(1);
        }

        bar_Container.transform.LookAt(main_camera.transform.position);


        if (Input.GetKeyDown(KeyCode.Z) && distance_to_player < 5f && SaveScript.stamina > 0.2)
        {
            roll_out = true;
        }

        if (enemy_is_alive == true)
        {
            Enemy_Outline();
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
            Enemy_Running();

            enemy_information = anim.GetCurrentAnimatorStateInfo(0);
            distance_to_player = Vector3.Distance(transform.position, player.transform.position);

            if (destination_run == true && Piglins == true)
            {
                chasing_Range = 0;
            }

            if (distance_to_player <= chasing_Range && destination_run == false)
            {
                CheckPlayerSight();

                if (player_is_inSight == true)
                {
                    //last_seen_position = player.transform.position;
                    search_Timer = 0f;
                    nav.destination = player.transform.position;
                    Main_Attack_System();
                }
                else if (!player_is_inSight && last_seen_position != Vector3.zero)
                {
                    NavMeshPath path = new NavMeshPath();
                    nav.CalculatePath(last_seen_position, path);
                    if (path.status != NavMeshPathStatus.PathComplete)
                    {
                        Look_Aroun_Yourself();
                    }
                    else if (look_for_player == true)
                    {
                        search_Timer += Time.deltaTime;
                        if (search_Timer >= time_for_search)
                        {
                            look_for_player = false;
                            search_Timer = 0f;
                        }
                        //Debug.Log(search_Timer);
                        Look_Aroun_Yourself();

                    }
                }

            }




            if (Goblin_Warrior == true && look_for_player == false)
            {
                Patrol();
                Correct_Aggression();
            }

            if (distance_to_player <= chasing_Range)
            {
                is_patroling = false;
            }

            if (roll_out == true && roll_is_active == false)
            {
                roll_is_active = true;
                Dodge();
                StartCoroutine(Reset_Roll_Triger());
            }

            //Debug.Log(Skeleton + " " + can_call_support + " " + sup_skill_used);
            if (Skeleton == true && can_call_support == true && sup_skill_used == false)
            {
                bool enemy_is_near_skeleton = Search_Enemy_Near_Skeleton();
                if (enemy_is_near_skeleton == false && distance_to_player <= 9f && SaveScript.agression_lvl > 0.7f)
                {
                    sup_skill_used = true;
                    SaveScript.agression_lvl -= 0.5f;
                    anim.SetTrigger("skill");
                    Spawn_Reinforcment();
                    StartCoroutine(Reset_Sup_Skill());
                    change_position = false;
                }
            }

            //curr_HP = was
            //full_hp - are
            if (curr_HP > full_HP)
            {
                anim.SetTrigger("hit");
                curr_HP = full_HP;
                RandomAudio_Hit();
                fillHealth = Convert.ToSingle(full_HP) / Convert.ToSingle(maxHP);
                Debug.Log(fillHealth);
                HP_bar.fillAmount = fillHealth;
                if (GetComponent<Enemy_Type>().enemyType == Enemy_Type.EnemyType.Piglin)
                {
                    piglin_was_hit = true;
                    chasing_Range = 60f;
                    StartCoroutine(Reset_Piglin_Renge());
                }

            }

            if (full_HP < maxHP / 2 && Piglins == true && destination_run == false)
            {
                destination_run = true;
                chasing_Range = 0;
                //Debug.Log("RUN AWAy");
                Run_Away();
            }

        }


        Vector3 dest = nav.destination;
        if (Vector3.Distance(current_enemy.transform.position, dest) <= 1.0f)
        {
            StartCoroutine(Reset_RunAwayTrigger());
        }

        //Debug.Log(nav.isStopped);
        //Debug.Log(Vector3.Distance(current_enemy.transform.position, dest));

        if (full_HP <= 1 && enemy_is_alive == true)
        {
            Enemy_is_Dead();
        }

    }

    public void Main_Attack_System()
    {
        if (is_patroling == false && Goblin_Warrior == true || Piglins == true && piglin_was_hit == true || Skeleton == true && change_position == false)
        {
            if (distance_to_player < attack_Range || distance_to_player > chasing_Range && destination_run != true) //if character is out of view  range or attack range - than enemy stop
            {
                if (destination_run != true)
                {
                    nav.isStopped = true;
                }

                //if(distance_to_player < chasing_Range)
                // {
                //Look_At_Player_Spherical_LERP();        //can be claimed as self-directed attack
                // }


                if (distance_to_player < attack_Range && enemy_information.IsTag("nonAttack") && SaveScript.is_invisible != true && destination_run != true)
                {

                    if (is_attacking == false)
                    {

                        is_attacking = true;
                        anim.SetTrigger("attack");
                        Look_At_Player_Spherical_LERP();   //little bit chunky
                    }
                }

                if (distance_to_player < attack_Range && enemy_information.IsTag("attack"))
                {

                    if (is_attacking == true)
                    {
                        is_attacking = false;
                    }
                }
            }
            else if (distance_to_player > attack_Range && enemy_information.IsTag("nonAttack") && !anim.IsInTransition(0))
            {
                if (SaveScript.is_invisible == false && destination_run == false)
                {
                    Go_To_Player();
                }

            }

        }
    }

    public void Go_To_Player()
    {
        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(transform.position, player.transform.position, NavMesh.AllAreas, path))
        {
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                nav.destination = player.transform.position;
                nav.isStopped = false;
            }
            else if (path.status == NavMeshPathStatus.PathPartial)
            {
                nav.destination = path.corners[path.corners.Length - 1];
                nav.isStopped = false;
            }
            else
            {
                nav.isStopped = true;
            }
        }
        else
        {
            nav.isStopped = true;
        }
        // Коригування швидкості агента, якщо він застряг
        if (nav.isStopped && nav.velocity.sqrMagnitude < 0.1f)
        {
            nav.speed = 0; // Зупинити агента
        }
        else
        {
            nav.speed = 3.5f; // Відновити швидкість агента
        }
        nav.stoppingDistance = stop_distance;
    }


    public void Look_At_Player_Spherical_LERP()
    {
        Vector3 Pos = (player.transform.position - transform.position).normalized;
        Quaternion PosRotation = Quaternion.LookRotation(new Vector3(Pos.x, 0, Pos.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, PosRotation, Time.deltaTime * rotation_speed);
    }

    public void Enemy_is_Dead()
    {
        SaveScript.agression_lvl = SaveScript.agression_lvl + 0.2f;
        enemy_is_alive = false;
        nav.isStopped = true;
        anim.SetTrigger("death");
        SaveScript.amount_of_chasing_enemies--;
        current_enemy.GetComponent<Outline>().enabled = false;
        is_outliner_active = false;
        nav.avoidancePriority = 1;
        StartCoroutine(Loot_Spawn());
    }

    public void Enemy_Outline()
    {
        //outline
        if (is_outliner_active == false)
        {
            is_outliner_active = true;
            if (SaveScript.spell_target == current_enemy)
            {
                current_enemy.GetComponent<Outline>().enabled = true;
            }
        }
        if (is_outliner_active == true)
        {
            if (SaveScript.spell_target != current_enemy)
            {
                current_enemy.GetComponent<Outline>().enabled = false;
                is_outliner_active = false;
            }
        }
        //
    }

    public void Enemy_Running()
    {
        x = nav.velocity.x;
        z = nav.velocity.z;
        velocitySpeed = new Vector2(x, z).magnitude;
        // velocitySpeed = x+z;      
        if (velocitySpeed == 0)
        {
            anim.SetBool("running", false);
            // Debug.Log("RUN = " + check);
        }
        else if (velocitySpeed != 0)
        {

            anim.SetBool("running", true);
            // check = anim.GetBool("running");
            is_attacking = false;
            //Debug.Log("running = " + check);

        }
    }

    public void RandomAudio_Hit()
    {
        int randomNumber = UnityEngine.Random.Range(1, 101);
        if (randomNumber > 0 && randomNumber < 33)
        {
            audio_Player.clip = get_Hit_SFX[0];
        }
        else if (randomNumber >= 33 && randomNumber < 66)
        {
            audio_Player.clip = get_Hit_SFX[1];
        }
        else if (randomNumber >= 66 && randomNumber < 101)
        {
            audio_Player.clip = get_Hit_SFX[2];
        }
        audio_Player.Play();
    }

    IEnumerator Loot_Spawn()
    {
        Enemy_Type enemy_type = GetComponent<Enemy_Type>();
        if (enemy_type.enemyType == Enemy_Type.EnemyType.Skelet)
        {
            yield return new WaitForSeconds(2);
        }
        else
        {
            yield return new WaitForSeconds(1);
        }
        Instantiate(Loot_from_Enemy, transform.position, transform.rotation);
        SaveScript.killed_enemy++;
        Destroy(gameObject, 0.2f);
    }

    public void Run_Away()
    {
        anim.SetBool("running", true);
        nav.isStopped = false;

        //int pos = Random.Range(0, 3);
        //nav.destination = escape_target_point[pos].transform.position;
        Calculate_Escape_Point();
        nav.speed = 1.8f;
        nav.destination = escape_point;

    }

    IEnumerator Reset_RunAwayTrigger()
    {
        yield return new WaitForSeconds(5);
        destination_run = false;
    }

    IEnumerator Reset_Roll_Triger()
    {
        yield return new WaitForSeconds(3f);
        roll_out = false;
        roll_is_active = false;
    }

    IEnumerator Reset_Piglin_Renge()
    {
        yield return new WaitForSeconds(7f);
        Look_At_Player_Spherical_LERP();
        piglin_was_hit = false;
        chasing_Range = 3f;
    }
    IEnumerator Reset_Sup_Skill()
    {
        yield return new WaitForSeconds(sup_skill_CD);
        sup_skill_used = false;
    }

    IEnumerator Wait_and_Attack()
    {
        yield return new WaitForSeconds(10f);
        Main_Attack_System();
    }
    public void Calculate_Escape_Point()
    {

        Vector3 escape_dir = Vector3.zero;
        float max_escape_distance = 0f;
        Vector3 player_dir = (player.transform.position - transform.position).normalized;

        for (int i = 0; i < 360; i += 5)
        {
            Vector3 new_direction = Quaternion.Euler(0, i, 0) * transform.forward;
            if (Vector3.Dot(new_direction.normalized, player_dir) < 0)
            {
                NavMeshHit hit;
                if (NavMesh.Raycast(transform.position, transform.position + new_direction * 100f, out hit, NavMesh.AllAreas))
                {
                    float distance = Vector3.Distance(transform.position, hit.position);
                    if (distance > max_escape_distance)
                    {
                        max_escape_distance = distance;
                        escape_dir = new_direction;
                    }
                }
            }
        }
        if (max_escape_distance > 0f && escape_dir != Vector3.zero)
        {
            NavMeshHit ray_hit_for_escape;
            if (NavMesh.SamplePosition(transform.position + escape_dir * max_escape_distance, out ray_hit_for_escape, max_escape_distance, NavMesh.AllAreas))
            {
                escape_point = ray_hit_for_escape.position;
            }
            else
            {
                escape_point = transform.position;
            }
        }

    }

    public void Set_Petrol_Destination()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * patrol_radius;
        randomDirection += patrol_main_obj.position;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, patrol_radius, -1);

        anim.SetBool("running", true);
        nav.isStopped = false;
        nav.destination = navHit.position;
    }

    public void Patrol()
    {
        is_patroling = true;
        if (!is_waiting && nav.remainingDistance <= 2.0f)
        {

            is_waiting = true;
            wait_timer = wait_time_at_point;
            is_patroling = false;
        }
        if (is_waiting)
        {
            wait_timer -= Time.deltaTime;

            if (wait_timer <= 0 || SaveScript.is_invisible == true)
            {

                is_waiting = false;
                Set_Petrol_Destination();
                nav.isStopped = false;
            }
        }

        if (SaveScript.is_invisible == true)
        {
            is_waiting = false;
            Set_Petrol_Destination();
            nav.isStopped = false;
        }
    }

    public void Dodge()
    {
        Vector3 playerDirection = player.transform.position - transform.position;
        playerDirection.Normalize();

        Vector3[] dodgeDirections = {
            -transform.forward,   // roll back
            transform.forward,    // roll forward
            -transform.right,     // roll left
            transform.right       // roll right
        };

        string[] dodgeTriggers = {
            "roll_B",
            "roll_F",
            "roll_L",
            "roll_R"
        };

        float[] weights = new float[dodgeDirections.Length];
        for (int i = 0; i < dodgeDirections.Length; i++)
        {
            Vector3 dodgePosition = transform.position + dodgeDirections[i] * dodgeDistance;
            if (NavMesh.SamplePosition(dodgePosition, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                // Calculate weight based on direction, distance to player, and aggression level
                float directionWeight = Vector3.Dot(playerDirection, dodgeDirections[i]);
                directionWeight = (1 - Mathf.Abs(directionWeight)) * (1 - aggression_lvl);
                weights[i] = directionWeight;
            }
            else
            {
                weights[i] = -1; // Invalid direction
            }
        }

        // Choose the direction with the highest weight
        int bestDirection = -1;
        float bestWeight = -1;
        for (int i = 0; i < weights.Length; i++)
        {
            if (weights[i] > bestWeight)
            {
                bestWeight = weights[i];
                bestDirection = i;
            }
        }

        if (bestDirection != -1)
        {
            anim.SetTrigger(dodgeTriggers[bestDirection]);
        }
    }
    public void Correct_Aggression()
    {
        if (curr_HP < 0.5f)
        {
            aggression_lvl += aggression_increase * Time.deltaTime;
        }
        else
        {
            aggression_lvl -= aggression_decrease * Time.deltaTime;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < 10f) // Example distance threshold
        {
            aggression_lvl += aggression_increase * Time.deltaTime;
            playerNearby = true;
        }
        else
        {
            playerNearby = false;
        }

        // Clamp the aggression level between min and max values
        aggression_lvl = Mathf.Clamp(aggression_lvl, min_aggression, max_aggression);

        //Debug.Log("Aggression Level: " + aggression_lvl);
    }

    public bool Search_Enemy_Near_Skeleton()
    {
        Collider[] all_colliders = Physics.OverlapSphere(transform.position, 10f);
        foreach (Collider collider in all_colliders)
        {
            if (collider.CompareTag("enemy") && collider.gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }

    public void Spawn_Reinforcment()
    {
        for (int i = 0; i < amount_of_reinforcment; i++)
        {

            Instantiate(support_enemy, GetRandom_Point_Around(), Quaternion.identity);
            SaveScript.amount_of_chasing_enemies++;
        }
    }

    public Vector3 GetRandom_Point_Around()
    {
        float angle = UnityEngine.Random.Range(0f, Mathf.PI * 2);
        float x = Mathf.Cos(angle) * 8f;
        float z = Mathf.Sin(angle) * 8f;
        Vector3 spawnPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
        return spawnPoint;
    }


    void CheckPlayerSight()
    {
        Vector3 player_dir = player.transform.position - transform.position;
        float angle = Vector3.Angle(player_dir, transform.forward);

        if (angle < 90f && player_dir.magnitude < distance_of_ray)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position + transform.up, player_dir.normalized, out hit, distance_of_ray))
            {
                Debug.DrawRay(transform.position, player_dir * 10f, Color.red);
                if (hit.transform == player.transform)
                {
                    Debug.DrawRay(transform.position, player_dir * 10f, Color.green);

                    Nearby_Enemy_Will_Know();
                    look_for_player = false;
                    player_is_inSight = true;
                    last_seen_position = player.transform.position;
                }
            }
        }
        else if (player_is_inSight)
        {
            Debug.DrawRay(transform.position, player_dir * 10f, Color.red);
            player_is_inSight = false;
            nav.SetDestination(last_seen_position);
            look_for_player = true;
        }
    }

    public void Nearby_Enemy_Will_Know()
    {
        try
        {
            Vector3 player_dir = player.transform.position - transform.position;
            Collider[] all_colliders = Physics.OverlapSphere(transform.position, group_brain_radius);
            foreach (var collider in all_colliders)
            {
                EnemyMovement raycast_system = collider.GetComponent<EnemyMovement>();
                if (raycast_system != null && collider.gameObject != gameObject)
                {
                    Debug.Log(raycast_system + " KNOW");
                    Debug.DrawRay(transform.position, player_dir * 10f, Color.green);
                    raycast_system.player_is_inSight = true;
                    raycast_system.look_for_player = false;
                    raycast_system.last_seen_position = player.transform.position;
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        
    }

    public void Look_Aroun_Yourself()
    {
        transform.Rotate(0, 120 * Time.deltaTime, 0);
    }
}
