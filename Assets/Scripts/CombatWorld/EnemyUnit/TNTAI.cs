using UnityEngine;

public class TNTAI : EnemyAI
{
    public GameObject tntPrefab;
    public Transform attackPoint;

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
            animator.Play("Original");
            Freeze();
        }
    }
    public override void HandleUnitActions()
    {
        target = enemyAttack.DetectTargetUnit(radius, unitLayerMask);
        if (target != null && target.transform.position.x >= transform.position.x)
        {
            animator.Play("Attack");
        }
        else
        {
            enemyMove.FollowThePath(wayPoints, speed);
            animator.Play("Run");
        }
    }

    public override void AttackEvent()
    {
        GameObject tntbullet = Instantiate(tntPrefab, attackPoint.position, Quaternion.identity);
        tntbullet.GetComponent<IBullet>().SetTarget(target);
        tntbullet.GetComponent<IBullet>().SetDame(dame);
    }
}
