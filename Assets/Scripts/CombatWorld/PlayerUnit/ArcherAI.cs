using UnityEngine;

public class ArcherAI : MonoBehaviour
{
    public GameObject bulletPrefabs;
    public Transform attackPoint;
    [HideInInspector] public GameObject target;
    public void Shoot()
    {
        GameObject newBullet = Instantiate(bulletPrefabs, attackPoint.position, Quaternion.identity);
        newBullet.GetComponent<IBullet>().SetTarget(target);
        gameObject.GetComponent<Animator>().Play("Idle");
    }

    public void Prepare(GameObject target)
    {
        this.target = target;
    }
}
