using UnityEngine;

public interface ITower
{
    public int Cost { get; }
    public float Radius { get; }
    public GameObject NewTower { get; }
    public void DetectTarget();
    public void Attack(GameObject target);
}
