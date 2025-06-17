using UnityEngine;

public class Metro : MonoBehaviour, IBullet
{
    public GameObject effectPrefab;
    public float speed = 2f;
    public int dame = 30;
    public GameObject target;
    public Vector3 endPoint = Vector3.zero;
    private void Start()
    {
        endPoint = GetRandomPos(target.transform.position, 1f, 1f);
        Debug.Log(endPoint);
    }
    private void Update()
    {
        MoveToTarget();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Instantiate(effectPrefab, transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<IEnemy>().TakeDame(dame);
            Destroy(this.gameObject);
        }
    }
    public void MoveToTarget()
    {
        if (target != null)
        {
            Vector2 direc = (endPoint - transform.position).normalized;
            float angle = Mathf.Atan2(direc.y, direc.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            transform.position = Vector2.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);

            float distance = Vector3.Distance(transform.position, endPoint);
            if (distance < 1.5f)
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
            if (Vector3.Distance(transform.position, endPoint) < 0.05f)
            {
                Instantiate(effectPrefab, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }

    }
    public Vector3 GetRandomPos(Vector3 pos, float xrange, float yrange)
    {
        float randomX = Random.Range(-xrange, xrange);
        float randomY = Random.Range(-yrange, yrange);
        return (pos + new Vector3(randomX, randomY, 0));
    }
    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    public void SetDame(int dame)
    {
        throw new System.NotImplementedException();
    }
}
