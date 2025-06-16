using UnityEngine;

public class TourchAI : EnemyAI
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
        HandleUnitActions();
    }
    public override void HandleUnitActions()
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

}
