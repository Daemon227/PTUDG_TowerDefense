using UnityEngine;
using UnityEngine.EventSystems;

public class LevelUpTower : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        BuyTowerUIManager.Instance.LevelUpTower();
    }

    
}
