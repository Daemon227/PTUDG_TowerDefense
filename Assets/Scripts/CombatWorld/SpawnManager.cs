using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Path Settup")]
    public List<Path> paths;
    // quan ly danh sach unit va vi tri spawn
    public List<Transform> SpawnPos;

    // quan ly theo turn data:
    public GameObject bossPrefab;
    public List<TurnData> turnDatas;
    private int currentTurn = 0;
    private List<GameObject> currentEnemyUnitPrefabs;

    // vi tri sinh quai hien tai
    private int currentPos = 0;

    // quan ly thoi gian spawn lien tiep giua 2 unit.
    private float timeToSpawn = 0.5f;
    private float currentTime = 0;

    // quan ly thoi gian delay giua 2 luot.
    private float delayTimeBetween2Turn = 35f;
    private float delayTimer = 0;

    //quan ly so luong unit.
    private List<GameObject> units = new List<GameObject>();
    private int limitedUnitCanSpawn = 0;

    private bool isNextTurn = false;// quan ly sinh turn moi nhanh

    //quan ly UI
    public TextMeshProUGUI turnText;

    private void Update()
    {
        HandleSpawnByTurn();
        turnText.text = "Turn: " + (currentTurn + 1) + "/" + turnDatas.Count;
    }
    public void SetTurn()
    {
        if (currentTurn < turnDatas.Count)
        {
            currentEnemyUnitPrefabs = turnDatas[currentTurn].enemyPrefabs;
            limitedUnitCanSpawn = turnDatas[currentTurn].limitEnemyNumber;
        }
        
    }
    public void SpawnEnemy()
    {
        currentPos = Random.Range(0, SpawnPos.Count);

        int randomUnit = Random.Range(0, currentEnemyUnitPrefabs.Count);
        GameObject enemyUnit = Instantiate(currentEnemyUnitPrefabs[randomUnit], SpawnPos[currentPos].position, Quaternion.identity);
        enemyUnit.GetComponent<EnemyAI>().SetWayPoint(paths[currentPos].wayPoints);
        units.Add(enemyUnit);
    }

    public void HandleSpawnByTurn()
    {
        if (units.Count == 0 || isNextTurn)
        {
            SetTurn();
        }
        if (units.Count < limitedUnitCanSpawn)
        {
            currentTime += Time.deltaTime;
            if (currentTime > timeToSpawn)
            {
                SpawnEnemy();
                currentTime = 0;
            }
        }
        else
        {
            delayTimer += Time.deltaTime;
            if (delayTimer >= delayTimeBetween2Turn)
            {
                units.Clear();
                currentTurn += 1;
                delayTimer = 0;
            }
        }
    }
}

[System.Serializable]
public class Path
{
    public List<Transform> wayPoints;
}

[System.Serializable]
public class TurnData
{
    public List<GameObject> enemyPrefabs;
    public int limitEnemyNumber;
}
