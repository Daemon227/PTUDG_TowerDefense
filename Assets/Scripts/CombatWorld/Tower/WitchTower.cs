using UnityEngine;

public class WitchTower : MonoBehaviour, ITower
{
    public int cost = 60;
    public int dame = 15;
    public LayerMask enemyLayerMask;
    public float radius = 3;
    public GameObject witch;
    public GameObject newTower;
    public bool canChangePos;
    public AudioClip audioClip;
    public string description = "Sát thương cao, tầm bắn rộng nhưng tốc độ bắn chậm";

    // kiem soat delay khi ban dan
    private GameObject target;
    private float delayTime = 1.5f;
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
            Debug.Log("Say hello");
            Debug.Log(target.name + " " + target.layer);
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
            witch.GetComponent<WitchAI>().Prepare(target);
            witch.GetComponent<Animator>().Play("Attack");
            isFirstTimeShoot = false;
        }
        else
        {
            currentTime += Time.deltaTime;
            if (currentTime > delayTime)
            {
                currentTime = 0;
                witch.GetComponent<WitchAI>().Prepare(target);
                witch.GetComponent<Animator>().Play("Attack");
            }
        }
    }
}
