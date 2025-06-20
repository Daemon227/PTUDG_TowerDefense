using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallPawn : MonoBehaviour, ISkill
{
    public int id = 0;
    public List<GameObject> pawnPrefabs = new List<GameObject>();
    public int numberpawn;
    public float countdown;
    public int cost = 20;
    public Sprite skillSprite;
    public string description;

    #region PUBLIC SOME PRIVATE INFOR
    public float CountDown => countdown;
    public int Cost => cost;
    public Sprite SkillSprite => skillSprite;
    public string Description => description;

    public int GetID => id;
    #endregion

    private void Start()
    {
        StartCoroutine(SpawnMetro());
    }
    public IEnumerator SpawnMetro()
    {
        for (int i = 0; i < numberpawn; i++)
        {
            int currentObject = Random.Range(0, pawnPrefabs.Count);
            float randomX = Random.Range(-1, 1.5f);
            float randomY = Random.Range(-1, 1.5f);
            Vector3 pos = transform.position + new Vector3(randomX, randomY, 0);

            GameObject metro = Instantiate(pawnPrefabs[currentObject], pos, Quaternion.identity);           
            yield return new WaitForSeconds(0.3f);
        }
        yield return null;
    }
}
