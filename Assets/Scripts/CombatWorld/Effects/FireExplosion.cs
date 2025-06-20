using System.Collections;
using UnityEngine;

public class FireExplosion : MonoBehaviour
{
    public GameObject fire;
    [HideInInspector] public int dame;
    private Vector3 spawnPos;
    void Start()
    {
        spawnPos = transform.position;
        StartCoroutine(SpawnFire());
    }

    public IEnumerator SpawnFire()
    {
        for (int i = 0; i< 3; i++)
        {
            Instantiate(fire, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(0.25f);
            spawnPos += new Vector3(1f, 0, 0);           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<IPlayerUnit>().TakeDame(dame);
        }
    }
}
