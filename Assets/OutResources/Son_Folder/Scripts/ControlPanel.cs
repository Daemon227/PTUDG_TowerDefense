using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject ShopPanel;
    public GameObject CollectorPanel;
    public GameObject EnemyCollectorPanel;
    public GameObject TowerCollectorPanel;
    public GameObject SupportItemPanel;
    public GameObject SettingPanel;
    public GameObject LevelSelecting;
    public GameObject BossCollectorPanel;

    private GameObject[] allPanels;

    void Awake()
    {
        allPanels = new GameObject[] {
            MenuPanel, ShopPanel, CollectorPanel,
            EnemyCollectorPanel, TowerCollectorPanel,
            SupportItemPanel, SettingPanel, LevelSelecting,
            BossCollectorPanel
        };
    }

    private void ShowOnly(GameObject[] allPanels, GameObject currentPanel)
    {
        foreach (var panel in allPanels)
            panel.SetActive(false);

        currentPanel.SetActive(true);
    }

    public void OpenShopPanel()
    {
        ShowOnly(allPanels,ShopPanel);
    }

    public void OpenCollectorPanel()
    {

        ShowOnly(allPanels,CollectorPanel);
        Debug.Log("Clicked");
    }

    public void OpenEnemyCollectorPanel()
    {
        ShowOnly(allPanels,EnemyCollectorPanel);
    }

    public void OpenTowerCollectorPanel()
    {
        ShowOnly(allPanels,TowerCollectorPanel);
    }


    public void OpenSupportItemPanel()
    {
        ShowOnly(allPanels,SupportItemPanel);
    }

    public void OpenSettingPanel()
    {
        ShowOnly(allPanels,SettingPanel);
    }

    public void OpenLevelSelecting()
    {
        ShowOnly(allPanels, LevelSelecting);
    }
    public void BackToMenu()
    {
        ShowOnly(allPanels, MenuPanel);
    }
    public void OpenBossPanel()
    {
        ShowOnly(allPanels, BossCollectorPanel);
    }

    void Start()
    {
        BackToMenu();
    }
}
