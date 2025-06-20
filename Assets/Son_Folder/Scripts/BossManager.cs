using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    [Header("Boss Data")]
    public BossData[] bossList;

    [Header("UI Panel Left")]
    public Text nameText, healthText, speedText, damageText,difficultText, descriptionText;
    public Image selectedBossImage;

    [Header("UI Panel Right")]
    public Button[] bossButtons;

    void Start()
    {
        for (int i = 0; i < bossButtons.Length; i++)
        {
            int index = i;
            bossButtons[i].onClick.AddListener(() => OnBossButtonClicked(index));
        }
    }

    public void OnBossButtonClicked(int index)
    {
        if (index < 0 || index >= bossList.Length) return;

        BossData boss = bossList[index];

        selectedBossImage.sprite = boss.bossSprite;
        nameText.text = boss.bossName;
        healthText.text = "HP: " + boss.health;
        speedText.text = "Tốc độ bắn: " + boss.speed;
        damageText.text = "Sát thương: " + boss.damage;
        difficultText.text = "Độ khó: " + boss.difficulty;
        descriptionText.text = boss.description;

        //tìm và thay ảnh
        Transform buttonTransform = bossButtons[index].transform;

        Transform questionImage = buttonTransform.Find("QuestionImage");
        Transform bossImage = buttonTransform.Find("TowerImage");

        if (questionImage != null)
            questionImage.gameObject.SetActive(false);

        if (bossImage != null)
        {
            Image img = bossImage.GetComponent<Image>();
            img.sprite = boss.bossSprite;
            bossImage.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("MonsterImage không tìm thấy trong button index: " + index);
        }
    }
}
