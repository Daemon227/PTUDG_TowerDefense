using UnityEngine;

public class RangerTower : MonoBehaviour, ITower
{
    public int cost = 50;
    public int dame = 5;
    public LayerMask enemyLayerMask;
    public float radius = 3;
    public GameObject acher;
    public GameObject newTower;
    public bool canChangePos = false;
    public AudioClip audioClip;
    public string description = "Sát thương thấp nhưng tốc độ bắn nhanh";

    // kiem soat delay khi ban dan
    private GameObject target;
    private float delayTime = 1f;
    private float currentTime = 2;
    private bool isFirstTimeShoot = true;

    #region PUBLIC SOME PRIVATE INFOR
    public int Cost => cost;
    public float Radius => radius;
    public GameObject NewTower => newTower;
    public bool CanChangePos => canChangePos;
    public AudioClip VoiceAudioClip => audioClip;
    public int Dame => dame;
    public string GetDescription => description;

    #endregion

    private void Update()
    {
        DetectTarget();       
    }
    public void DetectTarget()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, radius, enemyLayerMask);
        if (collider != null)
        {
            target = collider.gameObject;
            Attack(target);  
        }
        else
        {
            isFirstTimeShoot = true;
        }
    }
    public void Attack(GameObject target)
    {
        if (isFirstTimeShoot)
        {
            acher.GetComponent<ArcherAI>().Prepare(target);
            acher.GetComponent<Animator>().Play("Shoot Diagona Down");
            isFirstTimeShoot = false;
        }
        else
        {
            currentTime += Time.deltaTime;
            if (currentTime > delayTime)
            {
                currentTime = 0;
                acher.GetComponent<ArcherAI>().Prepare(target);
                acher.GetComponent<Animator>().Play("Shoot Diagona Down");
            }
        }
    }
}
