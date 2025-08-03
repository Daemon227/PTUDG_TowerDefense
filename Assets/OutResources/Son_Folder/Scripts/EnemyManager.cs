using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemy Data")]
    public EnemyData[] enemyList;

    [Header("UI Panel Left")]
    public Text nameText, healthText, speedText, damageText, difficultyText, description;
    public Image selectedEnemyImage;

    [Header("UI Panel Right")]
    public Button[] enemyButtons;

    void Start()
    {
        for (int i = 0; i < enemyButtons.Length; i++)
        {
            int index = i;
            enemyButtons[i].onClick.AddListener(() => OnEnemyButtonClicked(index));
        }
    }

    public void OnEnemyButtonClicked(int index)
    {
        if (index < 0 || index >= enemyList.Length) return;

        EnemyData enemy = enemyList[index];

        selectedEnemyImage.sprite = enemy.enemySprite;
        nameText.text = enemy.enemyName;
        healthText.text = "Máu: " + enemy.health;
        speedText.text = "Tốc độ: " + enemy.speed;
        damageText.text = "Sát thương: " + enemy.damage;
        difficultyText.text = "Độ khó: " + enemy.difficulty.ToString();
        description.text = enemy.description;

        Transform buttonTransform = enemyButtons[index].transform;

        Transform questionImage = buttonTransform.Find("QuestionImage");
        Transform monsterImage = buttonTransform.Find("MonsterImage");

        if (questionImage != null) questionImage.gameObject.SetActive(false);

        if (monsterImage != null)
        {
            Image img = monsterImage.GetComponent<Image>();
            img.sprite = enemy.enemySprite;
            monsterImage.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("MonsterImage không tìm thấy trong button index: " + index);
        }
    }
}
