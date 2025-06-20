using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SellTower : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI coinText;
    public GameObject sellPanel;
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

    public void OnPointerExit(PointerEventData eventData)
    {
        sellPanel.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        sellPanel.SetActive(true);
    }
}
