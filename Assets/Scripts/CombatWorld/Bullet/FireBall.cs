using UnityEngine;

public class FireBall : MonoBehaviour, IBullet
{
    public float speed = 5f;
    public int dame = 10;
    public GameObject target;
    private void Update()
    {
        MoveToTarget();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == target.gameObject)
        {
            collision.gameObject.GetComponent<IEnemy>().TakeDame(dame);
            Destroy(this.gameObject);
        }
    }

    public void MoveToTarget()
    {
        if (target != null)
        {
            Vector2 direc = (target.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direc.y, direc.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        else Destroy(gameObject);
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
}
