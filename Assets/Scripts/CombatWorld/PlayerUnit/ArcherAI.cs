using UnityEngine;

public class ArcherAI : MonoBehaviour
{
    public GameObject bulletPrefabs;
    public Transform attackPoint;
    [HideInInspector] public GameObject target;
    [HideInInspector] public int dame;

    private AudioSource audioSource;
    public AudioClip audioClip;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
        dame = GetComponentInParent<ITower>().Dame;
    }
    private void Update()
    {
        checkDir();
    }
    public void checkDir()// xoay theo enemy
    {
        if (target == null) return;
        if (target.transform.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    public void Shoot()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        GameObject newBullet = Instantiate(bulletPrefabs, attackPoint.position, Quaternion.identity);
        newBullet.GetComponent<IBullet>().SetTarget(target);
        newBullet.GetComponent<IBullet>().SetDame(dame);
        gameObject.GetComponent<Animator>().Play("Idle");
    }

    public void Prepare(GameObject target)
    {
        this.target = target;
    }
}
