using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SellTower : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI coinText;
    private void Update()
    {
        GameObject current = BuyTowerUIManager.Instance.GetCurrentObject();
        if (current == null) coinText.text = "0";
        else coinText.text = current.GetComponent<ITower>().Cost * 0.6f + "";
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        BuyTowerUIManager.Instance.SellTower();
    }
}
