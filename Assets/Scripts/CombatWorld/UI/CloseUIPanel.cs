using UnityEngine;
using UnityEngine.EventSystems;

public class CloseUIPanel : MonoBehaviour
{
    public GameObject buyPanel;
    public GameObject upgradePanel;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                buyPanel.SetActive(false);
                upgradePanel.SetActive(false);
            }
        }
    }
}
