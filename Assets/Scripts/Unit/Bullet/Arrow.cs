using UnityEngine;

public class Arrow : MonoBehaviour, IBullet
{
    public float speed = 5f;
    public GameObject target;
    private void Update()
    {
        MoveToTarget();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == target.gameObject)
        {
            Debug.Log("Booomm");
            Destroy(this.gameObject);
        }
    }

    public void MoveToTarget()
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        else Destroy(gameObject);
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
}
