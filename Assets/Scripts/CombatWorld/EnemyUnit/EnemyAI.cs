using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IEnemy
{
    public List<Transform> wayPoints;
    [SerializeField] private float radius = 1f;
    public int maxHP = 30;
    public int currentHp;
    public int coin = 15;
    public LayerMask layerMask;
    private EnemyMove enemyMove;
    private EnemyAttack enemyAttack;
    private EHpBar hpbar;

    private void Awake()
    {
        currentHp = maxHP;

        enemyMove = GetComponent<EnemyMove>();
        if (enemyMove == null) enemyMove = gameObject.AddComponent<EnemyMove>();     
        enemyAttack = GetComponent<EnemyAttack>();
        if (enemyAttack == null) enemyAttack = gameObject.AddComponent<EnemyAttack>();

        hpbar = GetComponentInChildren<EHpBar>();
        if (hpbar == null) return;
        hpbar.SetHpInfor(maxHP);
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
        this.currentHp -= dame;
        hpbar.FillHpBar(currentHp);
        if (this.currentHp <= 0)
        {
            CombatManager.Instance.TakeCoin(coin);
            Destroy(gameObject);
        }
    }
}
