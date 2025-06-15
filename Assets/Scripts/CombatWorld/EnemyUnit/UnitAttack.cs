using UnityEditor.Rendering;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    public GameObject DetectTargetUnit(float radius, LayerMask layerMask)
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, radius, layerMask);
        if (collider != null)
        {
            return collider.gameObject;
        }
        else
        {
            return null;
        }
    }

    public void AttackTarget()
    {
        Debug.Log("attack player Unit");
    }
}
