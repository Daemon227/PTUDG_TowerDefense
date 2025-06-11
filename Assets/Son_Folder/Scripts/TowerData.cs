using UnityEngine;

[CreateAssetMenu(fileName = "NewTower", menuName = "Tower Data")]
public class TowerData : ScriptableObject
{
    public string towerName;
    public Sprite towerSprite;
    public int price;
    public float rateSpeed;
    public int damage;
    public string description;
}
