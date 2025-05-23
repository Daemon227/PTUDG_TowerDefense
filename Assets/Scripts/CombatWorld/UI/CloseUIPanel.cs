using UnityEngine;
using UnityEngine.EventSystems;

public class CloseUIPanel : MonoBehaviour
{
    public GameObject panelToClose;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                panelToClose.SetActive(false);
            }
        }
    }
}
