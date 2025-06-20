using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TowerInforPanel : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dameText;
    public TextMeshProUGUI cointText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI motaText;

    public static TowerInforPanel Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void SetText(GameObject tower)
    {
        panel.SetActive(true);
        ITower t = tower.GetComponent<ITower>();
        if (t == null) return;

        nameText.text = tower.name;
        dameText.text = ": " + t.Dame;
        cointText.text = ": " + t.Cost;
        rangeText.text = ": " + t.Radius;
        motaText.text = "Mô tả: " + t.GetDescription;
    }
    public void ClosePanel()
    {
        panel.SetActive(false);
    }
}
