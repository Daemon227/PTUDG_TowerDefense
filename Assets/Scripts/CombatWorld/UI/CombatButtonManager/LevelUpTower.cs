using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelUpTower : MonoBehaviour, IPointerClickHandler
{
    public Image image;
    public Sprite state1;
    public Sprite state2;
    public TextMeshProUGUI coinText;

    private GameObject nextLevel;
    private void Update()
    {
        image.sprite = state1;

        nextLevel = BuyTowerUIManager.Instance.GetCurrentObject().GetComponent<ITower>().NewTower;
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
    public void OnPointerClick(PointerEventData eventData)
    {
        BuyTowerUIManager.Instance.LevelUpTower();
    }

    
}
