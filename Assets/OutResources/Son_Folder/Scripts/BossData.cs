using UnityEngine;

[CreateAssetMenu(fileName = "NewBoss", menuName = "Boss Data")]
public class BossData : ScriptableObject
{
    public string bossName;
    public Sprite bossSprite;
    public int health;
    public string speed;
    public int damage;
    public DifficultyLevel difficulty;
    public string description;
    public enum DifficultyLevel { Low, Medium, High }
}
