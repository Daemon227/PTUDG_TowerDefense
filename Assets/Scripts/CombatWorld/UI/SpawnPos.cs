using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnPos : MonoBehaviour
{
    public GameObject towerBuildPanel;

    public void OnMouseDown()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        towerBuildPanel.transform.position = screenPos;
        towerBuildPanel.SetActive(true);
    }
    public void OnMouseEnter()
    {
        gameObject.transform.localScale = Vector3.one * 1.5f;
    }

    public void OnMouseExit()
    {
        gameObject.transform.localScale = Vector3.one;
    }
}
