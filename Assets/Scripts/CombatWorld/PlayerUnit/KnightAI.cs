using System.Buffers;
using UnityEngine;

public class KnightAI : MonoBehaviour, IPlayerUnit
{
    [SerializeField] private float radius = 1.5f;
    public int maxHP = 30;
    public int currentHp;
    public int dame = 5;
    public LayerMask layerMask;

    // hieu ung chay mau
    public GameObject bloodEffect;

    private EHpBar hpbar;

    [HideInInspector] public Transform flagPos;
    private Vector3 currentPos;
    private Vector3 oldPos = Vector3.zero;

    private Vector3 fixedMoveTarget = Vector3.zero;
    private Transform currentMoveTarget = null;

    private UnitAttack unitAttack;
    public GameObject target;

    private Animator animator;
    // quan ly am thanh
    private AudioSource audioSource;
    public AudioClip attackClip;
    public AudioClip runClip;
    // quan ly debuff
    [HideInInspector] public bool canAction = true;
    [HideInInspector] public float timeEffect = 0;
    [HideInInspector] public float currentTime = 0;
    private void Awake()
    {
        currentHp = maxHP;

        unitAttack = GetComponent<UnitAttack>();
        if (unitAttack == null) unitAttack = gameObject.AddComponent<UnitAttack>();

        animator = GetComponent<Animator>();
        hpbar = GetComponentInChildren<EHpBar>();
        if (hpbar != null)
        {
            hpbar.SetHpInfor(maxHP);
        }
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
    }
  
    private void Update()
    {
        if (canAction)
        {
            if (IsChangePos())
            {
                GetRandomPos(flagPos, 1.5f, 1);
                MoveToTarget(fixedMoveTarget);
            }
            else
            {
                CheckDistanceToFlag();
                HandleUnitAction();
            }
        }
        else
        {
            animator.SetBool("IsAttack", false);
            animator.SetBool("IsRun", false);
            animator.SetBool("IsFreeze", true);
            Freeze(timeEffect);
        }
    }

    public void HandleUnitAction()
    {
        // Tìm mục tiêu nếu chưa có
        if (target == null)
        {
            animator.SetBool("IsAttack", false);
            animator.SetBool("IsRun", false);
            animator.SetBool("IsFreeze", false);
            target = unitAttack.DetectTargetUnit(radius, layerMask);
            return;
        }
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToTarget > radius + 1)
        {
            target = null;
            return;
        }
        if (distanceToTarget > radius * 0.5) // Nếu mục tiêu ở xa
        {
            GetRandomPos(target.transform, 1f, 1f);
            MoveToTarget(fixedMoveTarget);
        }
        else // Nếu mục tiêu ở gần
        {
            // Dừng di chuyển và tấn công
            animator.SetBool("IsAttack", true);
            animator.SetBool("IsRun", false);
            animator.SetBool("IsFreeze", false);

            // Xoay về hướng mục tiêu
            checkDir();

            Attack();
        }
    }
    public void CheckDistanceToFlag()
    {
        if (Vector3.Distance(transform.position, flagPos.position) > 2.5f)
        {
            oldPos = transform.position;
        }
    }

    public void checkDir()
    {
        if (target == null) return;
        if (target.transform.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void GetRandomPos(Transform pos, float xrange, float yrange)
    {
        if (currentMoveTarget != pos)
        {
            currentMoveTarget = pos;
            float randomX = Random.Range(-xrange, xrange);
            float randomY = Random.Range(-yrange, yrange);
            fixedMoveTarget = pos.position + new Vector3(randomX, randomY, 0);
        }
    }

    public void MoveToTarget(Vector3 pos)
    {
        if (Vector3.Distance(transform.position, pos) > 0.1f)
        {
            animator.SetBool("IsAttack", false);
            animator.SetBool("IsRun", true);
            animator.SetBool("IsFreeze", false);
            transform.position = Vector3.MoveTowards(transform.position, pos, 1.5f * Time.deltaTime);

            // Xoay theo hướng di chuyển
            if (transform.position.x - pos.x >= 0.5f)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            animator.SetBool("IsRun", false);
            oldPos = currentPos;
        }
    }

    public bool IsChangePos()
    {
        currentPos = flagPos.position;
        if (oldPos != currentPos)
        {
            currentMoveTarget = null;
            return true;
        }
        return false;
    }

    public void Attack()
    {
        if (target == null) return;
        animator.SetBool("IsAttack", true);
        animator.SetBool("IsRun", false);
        animator.SetBool("IsFreeze", false);
    }

    public void AttackEvent()
    {
        if (target != null)
        {
            audioSource.clip = attackClip;
            audioSource.Play();
            target.GetComponent<EnemyAI>().TakeDame(dame);
        }
    }

    public void TakeDame(int dame)
    {
        currentHp -= dame;
        GameObject blood = Instantiate(bloodEffect, transform.position, Quaternion.identity);
        if (hpbar != null)
        {
            hpbar.FillHpBar(currentHp);
        }
        if (currentHp <= 0)
        {
            gameObject.GetComponentInParent<KnightTower>().RemoveKnight(this.gameObject);
            Destroy(gameObject);
        }
    }

    public void Freeze(float timeEffect)
    {
        this.timeEffect = timeEffect;
        canAction = false;
        gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        currentTime += Time.deltaTime;
        if (currentTime > this.timeEffect)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            canAction = true;
            currentTime = 0;
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}