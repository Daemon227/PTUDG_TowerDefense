using NUnit.Framework;
using System.Collections;
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
    public LayerMask unitLayerMask;

    // hieu ung chay mau
    public GameObject bloodEffect;

    // hb bar
    [HideInInspector] public EHpBar hpbar;
    [HideInInspector] public GameObject target;

    // quan ly debuff
    [HideInInspector] public bool canAction = true;
    [HideInInspector] public float timeEffect = 0;
    [HideInInspector] public float currentTime = 0;
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
        GameObject blood = Instantiate(bloodEffect, transform.position, Quaternion.identity);
        //blood.transform.position = transform.position;
        if (this.currentHp <= 0)
        {
            CombatManager.Instance.TakeCoin(coin);
            Destroy(gameObject);
        }
    }

    public void Heal(int hp)
    {
        if (currentHp <= 0) return;
        currentHp += hp;
        if (currentHp > maxHP)
        {
            currentHp = maxHP;
        }
        hpbar.FillHpBar(currentHp);
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

    public void Freeze()
    {
        canAction = false;
        gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        currentTime += Time.deltaTime;
        if (currentTime > timeEffect)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            canAction = true;
            currentTime = 0;
        } 
    }
}
