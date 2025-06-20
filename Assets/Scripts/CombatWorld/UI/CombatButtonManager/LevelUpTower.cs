using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelUpTower : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    
    public Image image;
    public Sprite state1;
    public Sprite state2;
    public TextMeshProUGUI coinText;

    [Header("Quan ly infor panel")]
    public GameObject panelUpgrade;
    public GameObject panelMaxlevel;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI dameText;
    public TextMeshProUGUI rangeText;

    private GameObject nextLevel;
    private void Update()
    {
        image.sprite = state1;
        GameObject co = BuyTowerUIManager.Instance.GetCurrentObject();
        if (co == null) return;
        nextLevel = co.GetComponent<ITower>().NewTower;
        if(nextLevel == null)
        {
            image.sprite = state2;
            coinText.text = "Max";
        }
        else
        {
            coinText.text = nextLevel.GetComponent<ITower>().Cost+"";
        }
    }

    public void SetText()
    {
        GameObject co = BuyTowerUIManager.Instance.GetCurrentObject();
        if (co == null) return;

        nextLevel = co.GetComponent<ITower>().NewTower;

        if (nextLevel == null)
        {
            panelMaxlevel.SetActive(true);
        }
        else
        {

            int cost = nextLevel.GetComponent<ITower>().Cost;
            int dame = nextLevel.GetComponent<ITower>().Dame;
            float radius = nextLevel.GetComponent<ITower>().Radius;

            ITower currentTower = BuyTowerUIManager.Instance.GetCurrentObject().GetComponent<ITower>();      
            coinText.text = ": " + cost + " (+" + (cost - currentTower.Cost) + ")";
            dameText.text = ": " + dame + " (+" + (dame - currentTower.Dame) + ")";
            rangeText.text = ": " + radius + " (+" + (radius - currentTower.Radius) + ")";

            panelUpgrade.SetActive(true);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        BuyTowerUIManager.Instance.LevelUpTower();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetText();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        panelUpgrade.SetActive(false);
        panelMaxlevel.SetActive(false);
    }
}
