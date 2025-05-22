using UnityEditor.Rendering;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool DetectPlayerUnit(float radius, LayerMask layerMask)
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, radius, layerMask);
        if (collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AttackPlayer()
    {
        Debug.Log("attack player Unit");
    }
}
