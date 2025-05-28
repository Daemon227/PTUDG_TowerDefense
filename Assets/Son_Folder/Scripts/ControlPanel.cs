using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject ShopPanel;
    public GameObject CollectorPanel;
    public GameObject HeroCollectorPanel;
    public GameObject EnemyCollectorPanel;
    public GameObject TowerCollectorPanel;
    public GameObject HeroPanel;
    public GameObject SupportItemPanel;
    public GameObject SettingPanel;

    private GameObject[] allPanels;

    void Awake()
    {
        // Khởi tạo mảng chứa tất cả các panel
        allPanels = new GameObject[] {
            MenuPanel, ShopPanel, CollectorPanel,
            HeroCollectorPanel, EnemyCollectorPanel, TowerCollectorPanel,
            HeroPanel, SupportItemPanel, SettingPanel
        };
    }

    private void ShowOnly(GameObject[] allPanels, GameObject currentPanel)
    {
        foreach (var panel in allPanels)
            panel.SetActive(false);

        currentPanel.SetActive(true);
    }

    public void OpenHeroPanel()
    {
        ShowOnly(allPanels,HeroPanel);
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

    public void OpenHeroCollectorPanel()
    {
        ShowOnly(allPanels,HeroCollectorPanel);
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

    public void BackToMenu()
    {
        ShowOnly(allPanels,MenuPanel);
    }

    void Start()
    {
        BackToMenu(); // Mở menu khi bắt đầu game
    }
}
