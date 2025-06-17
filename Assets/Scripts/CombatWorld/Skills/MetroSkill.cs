using System.Collections;
using UnityEngine;

public class MetroSkill : MonoBehaviour, ISkill
{
    public Transform startPoint;
    public GameObject endPoint;
    public GameObject metroPrefabs;
    public int numberMetro;
    [HideInInspector] public bool isAttack = false;
    public float countdown;
    public float CountDown => countdown;

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
    public float CountDown { get; }
}
