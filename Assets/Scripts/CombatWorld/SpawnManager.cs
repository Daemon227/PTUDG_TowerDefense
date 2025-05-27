using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Path Settup")]
    public List<Path> paths;

    public List<Transform> SpawnPos;
    public List<GameObject> enemyUnitPrefabs;

    
    [SerializeField] private int currentPos = 0;

    // quan ly thoi gian spawn lien tiep giua 2 unit
    [SerializeField] private float timeToSpawn = 0.5f;
    [SerializeField] private float currentTime = 0;

    // quan ly thoi gian delay giua 2 luot
    private float delayTimeBetween2Turn = 5f;
    private float delayTimer = 0;

    private List<GameObject> units = new List<GameObject>();
    [SerializeField] private int limitedUnitCanSpawn = 5;
    private bool IsCanSpawn = true;
    private void Update()
    {
        CheckLimitedSpawn();
        if (IsCanSpawn)
        {
            currentTime += Time.deltaTime;
            if (currentTime > timeToSpawn)
            {
                SpawnEnemy();
                currentTime = 0;
            }
        }
    }
    public void SpawnEnemy()
    {
        currentPos = Random.Range(0, SpawnPos.Count);

        int randomUnit = Random.Range(0, enemyUnitPrefabs.Count);
        GameObject enemyUnit = Instantiate(enemyUnitPrefabs[randomUnit], SpawnPos[currentPos].position, Quaternion.identity);
        enemyUnit.GetComponent<EnemyAI>().SetWayPoint(paths[currentPos].wayPoints);
        units.Add(enemyUnit);
    }

    public void CheckLimitedSpawn()
    {
        if (units.Count <= limitedUnitCanSpawn) IsCanSpawn = true;
        else
        {
            IsCanSpawn = false;        
            delayTimer += Time.deltaTime;
            if (delayTimer>= delayTimeBetween2Turn)
            {
                if (limitedUnitCanSpawn <= 10) limitedUnitCanSpawn += 1;
                units.Clear();
                delayTimer = 0;
                IsCanSpawn = true;       
            }
        }
    }
}

[System.Serializable]
public class Path
{
    public List<Transform> wayPoints;
}
