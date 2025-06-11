using UnityEngine;
using UnityEngine.EventSystems;

public class CombatPanelManager : MonoBehaviour
{
    public GameObject buyPanel;
    public GameObject upgradePanel;

    public static CombatPanelManager Instance;

    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
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

    public void CloseAllUI()
    {
        buyPanel.SetActive(false);
        upgradePanel.SetActive(false);
    }

}
