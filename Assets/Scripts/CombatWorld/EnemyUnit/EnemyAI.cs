using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IEnemy
{
    public List<Transform> wayPoints;
    [SerializeField] private float radius = 1f;
    public int hp = 30;
    public int coin = 15;
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
    
    public void SetWayPoint(List<Transform> transforms)
    {
        this.wayPoints = transforms;
    }

    public void TakeDame(int dame)
    {
        this.hp -= dame;
        if (this.hp <= 0)
        {
            CombatManager.Instance.TakeCoin(coin);
            Destroy(gameObject);
        }
    }
}
