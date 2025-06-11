using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BuyTowerUIManager : MonoBehaviour
{
    public static BuyTowerUIManager Instance;
    private Dictionary<Vector3, bool> listSpawnPos = new Dictionary<Vector3, bool>();
    public Vector3 currentPosToSpawnTower = Vector3.zero;
    public SpawnPos currentSpawnPos = null;

    private void Start()
    {
        if (Instance!= null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    public Dictionary<Vector3, bool> ListSpawnPos { get => listSpawnPos; set => listSpawnPos = value; }

    public void SpawnTower(GameObject gameObject)
    {
        
        if (!listSpawnPos.ContainsKey(currentPosToSpawnTower))
        {
            return;
        }
        if (!listSpawnPos[currentPosToSpawnTower])
        {
            Instantiate(gameObject, currentPosToSpawnTower, Quaternion.identity);
            listSpawnPos[currentPosToSpawnTower] = true;
            if (currentSpawnPos != null)
            {
                currentSpawnPos.HideAfterSpawn();
            }
        }
    }
}
