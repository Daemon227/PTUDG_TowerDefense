using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IEnemy
{
    public List<Transform> wayPoints;
    [SerializeField] private float radius = 0.5f;
    public int maxHP = 30;  
    public int currentHp;
    public int coin = 15;
    public int dame = 2;

    public float speed;
    public LayerMask layerMask;
    private EnemyMove enemyMove;
    private UnitAttack enemyAttack;
    private EHpBar hpbar;

    private GameObject target;
    private Animator animator;
    private void Awake()
    {
        currentHp = maxHP;

        enemyMove = GetComponent<EnemyMove>();
        if (enemyMove == null) enemyMove = gameObject.AddComponent<EnemyMove>();     
        enemyAttack = GetComponent<UnitAttack>();
        if (enemyAttack == null) enemyAttack = gameObject.AddComponent<UnitAttack>();

        hpbar = GetComponentInChildren<EHpBar>();
        if (hpbar == null) return;
        hpbar.SetHpInfor(maxHP);

        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        target = enemyAttack.DetectTargetUnit(radius, layerMask);
        if (target != null)
        {          
            animator.Play("Attack");        
        }
        else
        {
            enemyMove.FollowThePath(wayPoints, speed);
            animator.Play("Run");
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
    public void AttackEvent()
    {
        if (target == null) return;
        target.GetComponent<KnightAI>().TakeDame(dame);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
