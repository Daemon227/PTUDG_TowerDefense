using UnityEngine;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{
    [Header("Tower Data")]
    public TowerData[] towerList; // Kéo ScriptableObjects vào đây

    [Header("UI Panel Left")]
    public Text nameText, priceText, ratespeedText, damageText, descriptionText;
    public Image selectedTowerImage;

    [Header("UI Panel Right")]
    public Button[] towerButtons;

    void Start()
    {
        for (int i = 0; i < towerButtons.Length; i++)
        {
            int index = i;
            towerButtons[i].onClick.AddListener(() => OnTowerButtonClicked(index));
        }
    }

    public void OnTowerButtonClicked(int index)
    {
        if (index < 0 || index >= towerList.Length) return;

        TowerData tower = towerList[index];

        // Hiển thị thông tin bên trái
        selectedTowerImage.sprite = tower.towerSprite;
        nameText.text = tower.towerName;
        priceText.text = "Giá: " + tower.price;
        ratespeedText.text = "Tốc độ bắn: " + tower.rateSpeed;
        damageText.text = "Sát thương: " + tower.damage;
        descriptionText.text = tower.description;

        // Panel phải: tìm và thay ảnh
        Transform buttonTransform = towerButtons[index].transform;

        Transform questionImage = buttonTransform.Find("QuestionImage");
        Transform towerImage = buttonTransform.Find("MonsterImage"); // hoặc đổi tên thành "TowerImage" nếu muốn rõ ràng hơn

        if (questionImage != null)
            questionImage.gameObject.SetActive(false);

        if (towerImage != null)
        {
            Image img = towerImage.GetComponent<Image>();
            img.sprite = tower.towerSprite; // đúng dữ liệu Tower
            towerImage.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("MonsterImage không tìm thấy trong button index: " + index);
        }
    }
}
