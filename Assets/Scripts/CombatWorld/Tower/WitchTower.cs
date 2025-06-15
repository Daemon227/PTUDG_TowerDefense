using UnityEngine;

public class WitchTower : MonoBehaviour, ITower
{
    public int cost = 50;
    public LayerMask enemyLayerMask;
    public float radius = 3;
    public GameObject witch;
    public GameObject newTower;
    public GameObject changPosPanel;

    // kiem soat delay khi ban dan
    private GameObject target;
    private float delayTime = 1.5f;
    private float currentTime = 2;
    private bool isFirstTimeShoot = true;

    public int Cost => cost;
    public float Radius => radius;
    public GameObject NewTower => newTower;
    public GameObject ChangePosPanel => changPosPanel;

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
