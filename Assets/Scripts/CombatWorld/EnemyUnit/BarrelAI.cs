using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BarrelAI : EnemyAI
{
    public GameObject boomEffect;

    private EnemyMove enemyMove;
    private UnitAttack enemyAttack;

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
        if (canAction)
        {
            HandleUnitActions();
        }
        else
        {
            if (currentHp > 0)
            {
                animator.SetBool("IsIn", false);
                animator.SetBool("IsAttack", false);
                animator.SetBool("IsFreeze", true);
                Freeze();
            }
            else
            {
                animator.SetBool("IsIn", false);
                animator.SetBool("IsAttack", true);
                animator.SetBool("IsFreeze", false);
            }        
        }
    }

    public override void HandleUnitActions()
    {
        target = enemyAttack.DetectTargetUnit(radius, unitLayerMask);
        if (currentHp > 0)
        {
            if (target != null)
            {
                animator.SetBool("IsIn", true);
                animator.SetBool("IsAttack", false);
                animator.SetBool("IsFreeze", false);
            }
            else
            {
                enemyMove.FollowThePath(wayPoints, speed);
                animator.SetBool("IsIn", false);
                animator.SetBool("IsAttack", false);
                animator.SetBool("IsFreeze", false);
            }
        }       
    }

    public override void TakeDame(int dame)
    {
        this.currentHp -= dame;
        hpbar.FillHpBar(currentHp);
        GameObject blood = Instantiate(bloodEffect, transform.position, Quaternion.identity);
        if (this.currentHp <= 0)
        {
            animator.SetBool("IsIn", false);
            animator.SetBool("IsAttack", true);
            animator.SetBool("IsFreeze", false);
        }
    }
    public override void AttackEvent()
    {
        GameObject boom = Instantiate(boomEffect, transform.position, Quaternion.identity);
        BoomEffect b = boom.GetComponent<BoomEffect>();
        if (b!= null)
        {
            b.radius = radius;
            b.dame = dame;
        }
        CombatManager.Instance.TakeCoin(coin);
        Destroy(gameObject);
    }
}
