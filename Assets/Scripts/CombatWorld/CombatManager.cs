using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("Infor Battle")]
    //[SerializeField] private int waveNumber = 5;
    public int hp = 10;
    public int coin = 150;

    //effect take dame
    public GameObject takeDamePanel;
    private bool isActive = false;
    private float delayTime = 0.2f;
    private float curentTime = 0;
    //
    public int Hp { get => hp; set => hp = value; }
    public int Coin { get => coin; set => coin = value; }

    public static CombatManager Instance;
    public bool IsGameOver = false;

    public GameObject winPanel;
    public GameObject loosePanel;

    private void Awake()
    {
        if (Instance != null && Instance!= this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Update()
    {
        if (IsGameOver)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
            ActiveTakeDamePanel();
    }
    public bool CheckCost(int amount)
    {
        if (coin >= amount)
        {
            return true;
        }
        else return false;
    }

    public void TakeCoin(int coin)
    {
        this.coin += coin;
    }

    public void TakeDame()
    {
        if(hp > 0)
        {
            this.hp -= 1;
            isActive = true;
        }

        if (hp == 0)
        {
            Debug.Log("GameOver");
            IsGameOver = true;
            loosePanel.SetActive(true);
        }
    }
    
    public void ActiveTakeDamePanel()
    {
        if (isActive)
        {
            takeDamePanel.SetActive(true);
            curentTime += Time.deltaTime;
            if (curentTime > delayTime)
            {
                takeDamePanel.SetActive(false);
                isActive = false;
                curentTime = 0;
            }
        }
    }
}
