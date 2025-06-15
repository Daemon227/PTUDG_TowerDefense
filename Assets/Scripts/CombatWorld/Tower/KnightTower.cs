using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class KnightTower : MonoBehaviour, ITower
{
    public int cost = 50;
    public LayerMask enemyLayerMask;
    public float radius = 3;
    public int numberKnight = 3;
    public GameObject knight;
    public GameObject newTower;
    public GameObject flag;
    public GameObject changPosPanel;

    private List<GameObject> knights = new List<GameObject>();
    // kiem soat delay khi hoi sinh knight sau khi chet
    private GameObject target;
    private float delayTime = 3f;
    private float currentTime = 2;
    private bool isFirstTimeShoot = true;

    public int Cost => cost;
    public float Radius => radius;
    public GameObject NewTower => newTower;
    public GameObject ChangePosPanel => changPosPanel;

    private void Update()
    {
        if (knights.Count < 3)
        {
            Attack(null);
        }
    }
    public void Attack(GameObject target)
    {
        currentTime += Time.deltaTime;
        if (currentTime > delayTime)
        {
            GameObject newKnight = Instantiate(knight, transform.position, Quaternion.identity);          
            newKnight.GetComponent<KnightAI>().flagPos = flag.transform;
            knights.Add(newKnight);
        }
        
    }

    public void DetectTarget()
    {
        throw new System.NotImplementedException();
    }
}
