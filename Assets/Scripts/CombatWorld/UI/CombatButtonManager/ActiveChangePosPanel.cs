using UnityEngine;

public class ActiveChangePosPanel : MonoBehaviour
{
    public GameObject changeposButton;
    private void Update()
    {
        GameObject currentO = BuyTowerUIManager.Instance.GetCurrentObject();
        if (currentO == null) return;

        ITower tower = currentO.GetComponent<ITower>();
        if (tower.CanChangePos)
        {
            changeposButton.SetActive(true);
        }
        else
        {
            changeposButton.SetActive(false);
        }
    }
}
