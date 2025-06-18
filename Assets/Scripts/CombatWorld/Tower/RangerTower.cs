using UnityEngine;

public class RangerTower : MonoBehaviour, ITower
{
    public int cost = 50;
    public LayerMask enemyLayerMask;
    public float radius = 3;
    public GameObject acher;
    public GameObject newTower;
    public GameObject changPosPanel;
    public AudioClip audioClip;
    // kiem soat delay khi ban dan
    private GameObject target;
    private float delayTime = 1f;
    private float currentTime = 2;
    private bool isFirstTimeShoot = true;
  
    public int Cost => cost;
    public float Radius => radius;
    public GameObject NewTower => newTower;
    public GameObject ChangePosPanel => changPosPanel;

    public AudioClip VoiceAudioClip => audioClip;

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
