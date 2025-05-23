using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnPos : MonoBehaviour
{
    public GameObject towerBuildPanel;

    public void OnMouseDown()
    {
        if (BuyTowerUIManager.Instance == null)
        {
            Debug.LogError("BuyTowerUIManager.Instance is null!");
            return;
        }
        if (!BuyTowerUIManager.Instance.ListSpawnPos.ContainsKey(transform.position))
        {
            BuyTowerUIManager.Instance.ListSpawnPos.Add(transform.position, false);
        }
        if (!BuyTowerUIManager.Instance.ListSpawnPos.GetValueOrDefault(transform.position))
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            towerBuildPanel.transform.position = screenPos;
            towerBuildPanel.SetActive(true);
            BuyTowerUIManager.Instance.currentPosToSpawnTower = transform.position;
            BuyTowerUIManager.Instance.currentSpawnPos = this;
        }
        
    }
    public void OnMouseEnter()
    {
        gameObject.transform.localScale = Vector3.one * 1.5f;
    }

    public void OnMouseExit()
    {
        gameObject.transform.localScale = Vector3.one;
    }
    public void HideAfterSpawn()
    {
        towerBuildPanel.SetActive(false);
        gameObject.SetActive(false); 
    }
}
