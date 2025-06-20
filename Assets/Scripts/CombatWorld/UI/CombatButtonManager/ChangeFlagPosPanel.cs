using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeFlagPosPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject panel;
    public void OnChangeFlagPosClick()
    {
        
        GameObject currentObject = BuyTowerUIManager.Instance.GetCurrentObject();
        if (currentObject == null) return;
        ChangeFlagPos changeFlagPos = currentObject.GetComponent<ChangeFlagPos>();
        if (changeFlagPos == null) return;
        Debug.Log("Chọn di chuyển cờ");
        changeFlagPos.canChange = true;   
        CombatPanelManager.Instance.CloseAllUI();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnChangeFlagPosClick();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        panel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        panel.SetActive(false);
    }
}
