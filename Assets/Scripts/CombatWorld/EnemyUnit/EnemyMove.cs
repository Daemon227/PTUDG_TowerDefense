using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class EnemyMove : MonoBehaviour
{
    private int currentWaypointIndex = 0;
    private bool isHasNewTargetPos = false;
    private Vector3 currentTargetPos = new Vector3();
    public void FollowThePath(List<Transform> wayPoints, float speed)
    {
        if (wayPoints.Count == 0) return;

        if (!isHasNewTargetPos)
        {
            SetRandomTargetPos(wayPoints);
            isHasNewTargetPos = true;
        }

        transform.position = Vector2.MoveTowards(transform.position, currentTargetPos, speed * Time.deltaTime);
        // Xoay theo hướng di chuyển
        if (transform.position.x - currentTargetPos.x >= 0.5f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (Vector2.Distance(transform.position, currentTargetPos) < 0.03f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= wayPoints.Count)
            {
                IBoss boss = gameObject.GetComponent<IBoss>();
                if(boss == null)
                {
                    CombatManager.Instance.TakeDame();
                    Destroy(this.gameObject);
                    return;
                }
                else
                {
                    CombatManager.Instance.IsWin = false;
                    CombatManager.Instance.IsGameOver = true;
                    Destroy(this.gameObject);
                    return;
                }
                
            }
            SetRandomTargetPos(wayPoints);
        }
    }
    public void SetRandomTargetPos(List<Transform> wayPoints)
    {
        float randomNumber = Random.Range(-0.3f, 0.3f);
        currentTargetPos = wayPoints[currentWaypointIndex].position + new Vector3(randomNumber, randomNumber, 0);
    }
}
