using UnityEngine;

public class TowerUI : MonoBehaviour
{
    public GameObject circle;
    private float radius;

    public GameObject upgradeTowerPanel;

    // delayTime truoc khi tat attackZone
    private float delayTimeBeforHideAttackZone = 2f;
    private float delayTimeHideAttackZone = 0;
    private bool isAttackZoneActive = true;

    private void Start()
    {
        radius = GetComponent<ITower>().Radius;

        SetScale();
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
