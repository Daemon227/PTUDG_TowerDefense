using UnityEngine;

public class RangerTower : MonoBehaviour, ITower
{
    public int cost = 50;
    public LayerMask enemyLayerMask;
    public GameObject bulletPrefabs;
    public float radius = 3;
    public GameObject circle;

    private GameObject target;
    private float delayTime = 1f;
    private float currentTime = 2;
    private bool isFirstTimeShoot = true;

    public int Cost => cost;

    private void Update()
    {
        DetectTarget();
        SetScale();
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
}
