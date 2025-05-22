using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IEnemy
{
    public List<Transform> wayPoints;
    [SerializeField] private float radius = 1f;
    public LayerMask layerMask;
    private EnemyMove enemyMove;
    private EnemyAttack enemyAttack;


    private void Awake()
    {
        enemyMove = GetComponent<EnemyMove>();
        if (enemyMove == null) enemyMove = gameObject.AddComponent<EnemyMove>();     
        enemyAttack = GetComponent<EnemyAttack>();
        if (enemyAttack == null) enemyAttack = gameObject.AddComponent<EnemyAttack>();
    }
    private void Update()
    {
        
        if (enemyAttack.DetectPlayerUnit(radius, layerMask))
        {
            enemyAttack.AttackPlayer();
        }
        else
        {
            enemyMove.FollowThePath(wayPoints);
        }
    }
    
}
