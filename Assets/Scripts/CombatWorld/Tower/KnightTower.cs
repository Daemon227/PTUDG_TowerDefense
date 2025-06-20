using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class KnightTower : MonoBehaviour, ITower
{
    public int cost = 50;
    public int dame = 3;
    public LayerMask enemyLayerMask;
    public float radius = 3;
    public int numberKnight = 3;
    public GameObject knight;
    public GameObject newTower;
    public GameObject flag;
    public bool canChangePos = true;
    public AudioClip audioClip;
    public string description = "Triệu hồi 3 đơn vị lính tấn công kẻ địch";

    private List<GameObject> knights = new List<GameObject>();
    // kiem soat delay khi hoi sinh knight sau khi chet
    private GameObject target;
    private float delayTime = 5f;
    private float currentTime = 0;
    private bool isFirstSpawm = true;

    #region PUBLIC SOME PRIVATE INFOR
    public int Cost => cost;
    public float Radius => radius;
    public GameObject NewTower => newTower;
    public bool CanChangePos => canChangePos;
    public AudioClip VoiceAudioClip => audioClip;
    public int Dame => dame;
    public string GetDescription => description;
 
    #endregion

    private void Update()
    {
        Attack(null);
    }
    public void Attack(GameObject target)
    {
        if (knights.Count < 3)
        {
            if (isFirstSpawm)
            {
                GameObject newKnight = Instantiate(knight, transform);
                newKnight.transform.position = transform.position;
                newKnight.GetComponent<KnightAI>().flagPos = flag.transform;
                knights.Add(newKnight);
            }
            else
            {
                currentTime += Time.deltaTime;
                if (currentTime > delayTime)
                {
                    GameObject newKnight = Instantiate(knight, transform);
                    newKnight.transform.position = transform.position;
                    newKnight.GetComponent<KnightAI>().flagPos = flag.transform;
                    knights.Add(newKnight);
                    currentTime = 0;
                }
            }
        }
        else
        {
            isFirstSpawm = false;
        }
    }

    public void DetectTarget()
    {
        throw new System.NotImplementedException();
    }

    public void RemoveKnight(GameObject knight)
    {
        if (!knights.Contains(knight)) return;
        knights.Remove(knight);
    }
}
