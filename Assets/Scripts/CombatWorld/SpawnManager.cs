﻿using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class SpawnManager : MonoBehaviour
{
    public int rubyCanRecive = 10;
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
    // kiem tra co boss chua
    private bool isHasBos = false;
    // quan ly thoi gian spawn lien tiep giua 2 unit.
    private float timeToSpawn = 0.5f;
    private float currentTime = 0;
    // quan ly thoi gian delay giua 2 luot.
    public float delayTimeBetween2Turn = 20f;
    private float delayTimer ;

    // quan ly thoi gian delay luot dau tien.
    public float firtTimeDelay = 15f;
    private float timcount;

    //quan ly so luong unit.
    private List<GameObject> units = new List<GameObject>();
    private int limitedUnitCanSpawn = 0;


    public bool isCanSpawn = false;
    public bool isNextTurn = false;

    //quan ly UI
    public TextMeshProUGUI turnText;
    public TextMeshProUGUI notifiText;

    private void Start()
    {
        delayTimer = delayTimeBetween2Turn;
        timcount = firtTimeDelay;
    }
    private void Update()
    {
        FirstSettup();
        if(isCanSpawn && currentTurn < turnDatas.Count)
        {
            HandleSpawnByTurn();
            SetNextTurn();
        }
        turnText.text = "Lượt: " + (currentTurn + 1) + "/" + turnDatas.Count;

    }
    public void FirstSettup()
    {
        if (isCanSpawn == false)
        {
            timcount -= Time.deltaTime;
            notifiText.text = "Đợt tấn công đầu tiên sẽ bắt đầu sau: " + timcount.ToString("F0") + " giây";
            if (timcount <= 0)
            {
                notifiText.transform.parent.gameObject.SetActive(false);
                isCanSpawn = true;
            }
        }
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

        if (currentTurn == turnDatas.Count - 1 && !isHasBos)
        {
            GameObject boss = Instantiate(bossPrefab, SpawnPos[currentPos].position, Quaternion.identity);
            boss.GetComponent<EnemyAI>().SetWayPoint(paths[currentPos].wayPoints);
            isHasBos = true;

            AudioManager.Instance.isChangeAudioClip = true;
        }
    }

    public void HandleSpawnByTurn()
    {
        if (units.Count == 0)
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
            isNextTurn = true;
        }
    }
    public void SetNextTurn()
    {
        if (isNextTurn && currentTurn < turnDatas.Count -1)
        {
            delayTimer -= Time.deltaTime;
            notifiText.transform.parent.gameObject.SetActive(true);
            notifiText.gameObject.SetActive(true);
            notifiText.text = "Đợt tấn công tiếp theo sẽ bắt đầu sau: " + delayTimer.ToString("F0") + " giây";
            if (delayTimer <= 0)
            {
                units.Clear();
                currentTurn += 1;
                delayTimer = delayTimeBetween2Turn;
                notifiText.gameObject.SetActive(false);
                isNextTurn = false;
            }
        }
        else
        {
            notifiText.transform.parent.gameObject.SetActive(false);
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
