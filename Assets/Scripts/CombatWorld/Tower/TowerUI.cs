using UnityEngine;

public class TowerUI : MonoBehaviour
{
    public GameObject circle;
    private float radius;
    private GameObject upgradeTowerPanel;
    private GameObject newTower;
    //private GameObject changePosPanel;

    // delayTime truoc khi tat attackZone
    private float delayTimeBeforHideAttackZone = 2f;
    private float delayTimeHideAttackZone = 0;
    private bool isAttackZoneActive = true;


    private void Start()
    {
        radius = GetComponent<ITower>().Radius;
        newTower = GetComponent<ITower>().NewTower;
        //changePosPanel = GetComponent<ITower>().ChangePosPanel;
        SetScale();

        upgradeTowerPanel = CombatPanelManager.Instance.upgradePanel;
    }
    private void Update()
    {
        if (isAttackZoneActive)
        {
            HideAttackZone();
        }
    }

    public void SetScale()
    {
        if (circle == null) return;
        float scale = radius / 0.5f;
        circle.transform.localScale = Vector3.one * scale;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void OnMouseEnter()
    {
        circle.SetActive(true);
    }
    public void OnMouseExit()
    {
        if (isAttackZoneActive == false)
        {
            circle.SetActive(false);
        }
    }

    public void OnMouseDown()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        upgradeTowerPanel.transform.position = screenPos;
        upgradeTowerPanel.SetActive(true);

        BuyTowerUIManager.Instance.currentPosToSpawnTower = transform.position;
        BuyTowerUIManager.Instance.newTower = newTower;
    }

    public void HideAttackZone()
    {
        delayTimeHideAttackZone += Time.deltaTime;
        if (delayTimeHideAttackZone > delayTimeBeforHideAttackZone)
        {
            isAttackZoneActive = false;
            circle.SetActive(false);
            delayTimeHideAttackZone = 0;
        }
    }
}
