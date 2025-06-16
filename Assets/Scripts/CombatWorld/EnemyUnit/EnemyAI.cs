using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IEnemy
{
    public List<Transform> wayPoints;
    public float radius = 0.5f;
    public int maxHP = 30;  
    public int currentHp = 30;
    public int coin = 15;
    public int dame = 2;
    public float speed;
    public LayerMask layerMask;
    [HideInInspector] public EHpBar hpbar;
    [HideInInspector] public GameObject target;

    public virtual void HandleUnitActions()
    {
       
    }

    public void SetWayPoint(List<Transform> transforms)
    {
        this.wayPoints = transforms;
    }

    public virtual void TakeDame(int dame)
    {
        this.currentHp -= dame;
        hpbar.FillHpBar(currentHp);
        if (this.currentHp <= 0)
        {
            CombatManager.Instance.TakeCoin(coin);
            Destroy(gameObject);
        }
    }
    public virtual void AttackEvent()
    {
        if (target == null) return;
        target.GetComponent<KnightAI>().TakeDame(dame);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
