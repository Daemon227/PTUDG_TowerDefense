using UnityEngine;

public interface ITower
{
    public int Cost { get; }
    public void DetectTarget();
    public void Attack(GameObject target);
}
