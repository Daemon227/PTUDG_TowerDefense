using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MetroSkill : MonoBehaviour, ISkill
{
    public Transform startPoint;
    public GameObject endPoint;
    public GameObject metroPrefabs;
    public int numberMetro;
    [HideInInspector] public bool isAttack = false;
    public float countdown;
    public int cost = 20;
    public Sprite skillSprite;
    public string description;
    public float CountDown => countdown;
    public int Cost => cost;
    public Sprite SkillSprite => skillSprite;
    public string Description => description;

    private void Start()
    {
        StartCoroutine(SpawnMetro());
    }
    public IEnumerator SpawnMetro()
    {
        if (isAttack)
        {
            for (int i = 0; i < numberMetro; i++)
            {
                GameObject metro = Instantiate(metroPrefabs, startPoint.position, Quaternion.identity);
                metro.GetComponent<IBullet>().SetTarget(endPoint);
                yield return new WaitForSeconds(0.3f);
            }
            isAttack = false;
            yield return null;
        }
    }
}

public interface ISkill
{
    public Sprite SkillSprite { get; }
    public float CountDown { get; }
    public int Cost { get; }
    public string Description { get; }
}
