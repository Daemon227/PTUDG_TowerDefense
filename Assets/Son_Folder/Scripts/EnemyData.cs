using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy/Create New Enemy")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public Sprite enemySprite;
    public int health;
    public float speed;
    public int damage;
    public DifficultyLevel difficulty;
    public string description;
    public enum DifficultyLevel { Low, Medium, High }
}
