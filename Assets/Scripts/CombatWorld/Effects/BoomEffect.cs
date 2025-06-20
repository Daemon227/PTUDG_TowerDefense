using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BoomEffect : MonoBehaviour
{
    [HideInInspector] public float radius;
    [HideInInspector] public int dame;

    private void Start()
    {
        StartCoroutine(Attack());
    }
    public IEnumerator Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius * 1.5f);
        foreach (var c in colliders)
        {
            if (c.CompareTag("Player"))
            {
                c.gameObject.GetComponent<IPlayerUnit>().TakeDame(dame);
            }
            if (c.CompareTag("Enemy"))
            {
                c.gameObject.GetComponent<EnemyAI>().TakeDame(dame);
            }
        }
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
