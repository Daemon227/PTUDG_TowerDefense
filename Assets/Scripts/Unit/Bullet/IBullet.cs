using UnityEngine;

public interface IBullet
{
    public void SetTarget(GameObject target);
    public void MoveToTarget();
}
