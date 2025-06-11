using UnityEngine;
using UnityEngine.EventSystems;

public class SellTower : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        BuyTowerUIManager.Instance.SellTower();
    }
}
