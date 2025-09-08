using System.Collections;
using UnityEngine;

public class CthuluAI : EnemyAI, IBoss
{ 
    public GameObject skillEffect;
    private EnemyMove enemyMove;
    private UnitAttack enemyAttack;

    private Animator animator;

    private float currentSpeed;
    private bool isDead = false;

    // delay skill
    public float countdown = 15f;
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
            if (isDead) return;
            if (currentHp <= 0)
            {
                enemyMove.FollowThePath(wayPoints, speed);
                animator.SetTrigger("IsDead");
                isDead = true;
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
        UseSkill();
        if (this.currentHp <= 0)
        {
            animator.SetTrigger("IsDead");
        }
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
            isUsingSkill = true;
            skillEffect.SetActive(true);
            animator.SetTrigger("IsUseSkill");
            animator.SetBool("IsFly", true);
            Debug.Log("UseSkill");
            currentSpeed = speed;
            speed = 0;
            timer = countdown;
        }
    }

    public void UseSkillEvent()
    {
        int value = (int)(maxHP * 0.05f);
        Heal(value);        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 3.5f);
        foreach (var c in colliders)
        {
            if (c.CompareTag("Enemy"))
            {
                //buff cho enemy
                c.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                EnemyAI e = c.gameObject.GetComponent<EnemyAI>();
                e.Heal(50);
                e.speed += e.speed * 0.1f;
                e.dame += (int)(e.dame * 0.2);
            }
        }
    }
    public void UseSkillEventLastTime()
    {
        int value = (int)(maxHP * 0.05f);
        Heal(value);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 3.5f);
        foreach (var c in colliders)
        {
            if (c.CompareTag("Enemy"))
            {
                //buff cho enemy
                c.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                EnemyAI e = c.gameObject.GetComponent<EnemyAI>();
                e.Heal(50);
                e.speed += e.speed * 0.1f;
                e.dame += (int)(e.dame * 0.2);
            }
        }
        skillEffect.SetActive(false);
        speed = currentSpeed;
        isUsingSkill = false;
        animator.SetBool("IsFly", false);
    }
}
