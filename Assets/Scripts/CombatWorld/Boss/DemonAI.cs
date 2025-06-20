using System.Buffers;
using System.Linq;
using UnityEngine;

public class DemonAI : EnemyAI, IBoss
{
    public GameObject attackEffect;
    public Transform attackPoint;
    public LayerMask towerLayerMask;

    private EnemyMove enemyMove;
    private UnitAttack enemyAttack;
    private Animator animator;

    private float currentSpeed;

    // delay skill
    public float countdown = 30f;
    private float timer = 0;

    private bool isUsingSkill = false;

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

        timer = countdown;
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
                animator.SetTrigger("IsDead");
            }
            else
            {
                animator.SetBool("IsWalk", false);
                animator.SetBool("IsFreeze", true);
                animator.SetBool("IsAttack", false);
                Freeze();
            }
        }
    }
    public override void HandleUnitActions()
    {
        target = enemyAttack.DetectTargetUnit(radius, unitLayerMask);
        if (currentHp > 0)
        {
            UseSkill();
            checkDir();
            if (target != null)
            {
                animator.SetBool("IsFreeze", false);
                animator.SetBool("IsWalk", false);
                animator.SetBool("IsAttack", true);
            }
            else
            {
                enemyMove.FollowThePath(wayPoints, speed);
                animator.SetBool("IsFreeze", false);
                animator.SetBool("IsWalk", true);
                animator.SetBool("IsAttack", false);
            }
        }
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
    public override void TakeDame(int dame)
    {
        this.currentHp -= dame;
        hpbar.FillHpBar(currentHp);
        GameObject blood = Instantiate(bloodEffect, transform.position, Quaternion.identity);
        if (this.currentHp <= 0)
        {
            animator.SetTrigger("IsDead");
        }
    }
    public override void AttackEvent()
    {
        GameObject attack = Instantiate(attackEffect, transform);
        attack.transform.position = attackPoint.position;
        attack.GetComponent<FireExplosion>().dame = dame;
    }
    public void DeadEvent()
    {
        CombatManager.Instance.IsWin = true;
        CombatManager.Instance.IsGameOver = true;
        Destroy(gameObject);
    }

    public void UseSkill()
    {
        timer -= Time.deltaTime;
        if (timer < 0 && !isUsingSkill)
        {
            // quay ve phia tru
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius * 3f, towerLayerMask);
            if (colliders.Length > 0)
            {
                if (colliders[0].transform.position.x < transform.position.x)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }
            
            // use skill
            isUsingSkill = true;
            animator.SetTrigger("IsUseSkill");
            animator.SetBool("IsBack", false);
            Debug.Log("UseSkill");
            currentSpeed = speed;
            speed = 0;
            timer = countdown;
        }
    }

    public void UseSkillEvent()
    {
        int value = (int)(maxHP * 0.2f);
        Heal(value);       
        speed = currentSpeed;
        isUsingSkill = false;
        animator.SetBool("IsBack", true);
    }
    
}
