using UnityEngine;

public class MinotaurAI : EnemyAI
{
    public GameObject boomEffect;
    public GameObject skillEffect;
    private EnemyMove enemyMove;
    private UnitAttack enemyAttack;

    private Animator animator;

    private float currentSpeed;

    private bool canUseSkill = true;

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
                animator.SetBool("IsUseSkill", false);
            }
            else
            {
                animator.SetBool("IsWalk", false);
                animator.SetBool("IsDead", false);
                animator.SetBool("IsAttack", false);
                animator.SetBool("IsFreeze", true);
                animator.SetBool("IsUseSkill", false);
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
                animator.SetBool("IsUseSkill", false);  
            }
            else
            {
                enemyMove.FollowThePath(wayPoints, speed);
                animator.SetBool("IsAttack", false);
                animator.SetBool("IsFreeze", false);
                animator.SetBool("IsDead", false);
                animator.SetBool("IsWalk", true);
                animator.SetBool("IsUseSkill", false);
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
            animator.SetBool("IsAttack", false);
            animator.SetBool("IsWalk", false);
            animator.SetBool("IsFreeze", false);
            animator.SetBool("IsDead", true);
            animator.SetBool("IsUseSkill", false);

            transform.localScale = Vector3.one * 2;
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
    public void DeadEvent()
    {
        
        Instantiate(boomEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    
    public void UseSkill()
    {
        if(currentHp <= maxHP/2 && canUseSkill)
        {
            animator.SetTrigger("UseSkill");
            Debug.Log("UseSkill");
            currentSpeed = speed;
            speed = 0;
            canUseSkill = false;
        }
    }

    public void UseSkillEvent()
    {
        currentHp = maxHP;
        hpbar.FillHpBar(currentHp);
        skillEffect.SetActive(true);
        transform.localScale += new Vector3(0.5f, 0.5f, 0f);
        speed = currentSpeed * 1.5f;
        dame += dame;
        Debug.Log("Hoi mau");
    }
}
