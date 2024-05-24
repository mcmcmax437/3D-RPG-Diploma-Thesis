using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Type : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemyType enemyType;

    public enum EnemyType
    {
        Goblin,
        Golem,
        Piglin,
        Skelet
    }
    
}
