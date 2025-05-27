using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    

    [Header("Infor Battle")]
    [SerializeField] private int waveNumber = 5;
    [SerializeField] private int hp = 10;
    [SerializeField] private int coin = 150;

    public int WaveNumber { get => waveNumber; set => waveNumber = value; }
    public int Hp { get => hp; set => hp = value; }
    public int Coin { get => coin; set => coin = value; }

    public static CombatManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance!= this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public bool CheckCost(int amount)
    {
        if (coin >= amount)
        {
            return true;
        }
        else return false;
    }
}
