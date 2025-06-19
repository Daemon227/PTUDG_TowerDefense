using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class TowerSelectedSpawn : MonoBehaviour, IPointerClickHandler
{
    public GameObject towerPrefab;
    public TextMeshProUGUI coinText;
    private void Start()
    {
        coinText.text = towerPrefab.GetComponent<ITower>().Cost + "";
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        int amount = towerPrefab.GetComponent<ITower>().Cost;
        if (CombatManager.Instance.CheckCost(amount))
        {
            BuyTowerUIManager.Instance.SpawnTower(towerPrefab);
            CombatManager.Instance.Coin -= amount;
            Debug.Log("Money: "+ CombatManager.Instance.Coin);
        }
        else
        {
            Debug.Log("not enought money");
        }
    }
}

