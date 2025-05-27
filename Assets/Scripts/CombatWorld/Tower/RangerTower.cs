using UnityEngine;

public class RangerTower : MonoBehaviour, ITower
{
    public int cost = 50;
    public LayerMask enemyLayerMask;
    public GameObject bulletPrefabs;
    public float radius = 3;
    public GameObject circle;

    // kiem soat delay khi ban dan
    private GameObject target;
    private float delayTime = 1f;
    private float currentTime = 2;
    private bool isFirstTimeShoot = true;

    // delayTime truoc khi tat attackZone
    private float delayTimeBeforHideAttackZone = 2f;
    private float delayTimeHideAttackZone = 0;
    private bool isAttackZoneActive = true;
    public int Cost => cost;

    private void Start()
    {
        SetScale();    
    }

    private void Update()
    {
        DetectTarget();
        
        if (isAttackZoneActive)
        {
            HideAttackZone();
        }       
    }
    public void DetectTarget()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, radius, enemyLayerMask);
        if (collider != null)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            target = collider.gameObject;
            Attack(target);  
        }
        else
        {
            isFirstTimeShoot = true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }
    public void Attack(GameObject target)
    {
        if (isFirstTimeShoot)
        {
            GameObject newBullet = Instantiate(bulletPrefabs, transform.position, Quaternion.identity);
            newBullet.GetComponent<IBullet>().SetTarget(target);
            isFirstTimeShoot = false;
        }
        else
        {
            currentTime += Time.deltaTime;
            if (currentTime > delayTime)
            {
                currentTime = 0;
                GameObject newBullet = Instantiate(bulletPrefabs, transform.position, Quaternion.identity);
                newBullet.GetComponent<IBullet>().SetTarget(target);
            }
        }
    }

    public void SetScale()
    {
        if (circle == null) return;
        float scale = radius / 0.75f;
        circle.transform.localScale = Vector3.one * scale;    
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void OnMouseEnter()
    {
        circle.SetActive(true);
    }
    public void OnMouseExit()
    {
        if(isAttackZoneActive == false)
        {
            circle.SetActive(false);
        }
    }

    public void HideAttackZone()
    {
        delayTimeHideAttackZone += Time.deltaTime;
        if(delayTimeHideAttackZone > delayTimeBeforHideAttackZone)
        {
            isAttackZoneActive = false;
            circle.SetActive(false);
            delayTimeHideAttackZone = 0;
        }
    }
}
