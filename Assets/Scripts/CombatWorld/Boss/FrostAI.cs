using UnityEngine;

public class FrostAI : EnemyAI, IBoss
{
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
            if (currentHp <= 0)
            {
                enemyMove.FollowThePath(wayPoints, speed);
                animator.SetBool("IsAttack", false);
                animator.SetBool("IsFreeze", false);
                animator.SetBool("IsWalk", false);
                animator.SetBool("IsDead", true);
            }
            else
            {
                animator.SetBool("IsWalk", false);
                animator.SetBool("IsDead", false);
                animator.SetBool("IsAttack", false);
                animator.SetBool("IsFreeze", true);
                Freeze();
            }
        }
        
    }
    public override void HandleUnitActions()
    {
        target = enemyAttack.DetectTargetUnit(radius, unitLayerMask);
        if (currentHp > 0)
        {
            checkDir();
            if (target != null)
            {
                animator.SetBool("IsWalk", false);
                animator.SetBool("IsFreeze", false);
                animator.SetBool("IsDead", false);
                animator.SetBool("IsAttack", true);

            }
            else
            {
                enemyMove.FollowThePath(wayPoints, speed);
                animator.SetBool("IsAttack", false);
                animator.SetBool("IsFreeze", false);
                animator.SetBool("IsDead", false);
                animator.SetBool("IsWalk", true);
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
            animator.SetBool("IsAttack", false);
            animator.SetBool("IsWalk", false);
            animator.SetBool("IsFreeze", false);
            animator.SetBool("IsDead", true);
        }
    }
    public void DeadEvent()
    {
        CombatManager.Instance.IsWin = true;
        CombatManager.Instance.IsGameOver = true;
        Destroy(gameObject);
    }
    public override void AttackEvent()
    {
        if (target == null) return;
        target.GetComponent<KnightAI>().TakeDame(dame);
        target.GetComponent<KnightAI>().timeEffect = 2f;
        target.GetComponent<KnightAI>().Freeze();
    }
    public void checkDir()
    {
        if (target == null)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            return;
        }
        if (target.transform.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
