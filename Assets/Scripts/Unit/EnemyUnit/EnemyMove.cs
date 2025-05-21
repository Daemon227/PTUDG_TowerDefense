using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private int currentWaypointIndex = 0;
    private bool isHasNewTargetPos = false;
    private Vector3 currentTargetPos = new Vector3();
    public void FollowThePath(List<Transform> wayPoints)
    {
        if (wayPoints.Count == 0) return;

        if (!isHasNewTargetPos)
        {
            SetRandomTargetPos(wayPoints);
            isHasNewTargetPos = true;
        }

        transform.position = Vector2.MoveTowards(transform.position, currentTargetPos, 2 * Time.deltaTime);

        if (Vector2.Distance(transform.position, currentTargetPos) < 0.03f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= wayPoints.Count)
            {
                currentWaypointIndex = 0;
                transform.position = wayPoints[currentWaypointIndex].position;
            }

            SetRandomTargetPos(wayPoints);
        }
    }
    public void SetRandomTargetPos(List<Transform> wayPoints)
    {
        float randomNumber = Random.Range(-1, 1);
        currentTargetPos = wayPoints[currentWaypointIndex].position + new Vector3(randomNumber, randomNumber, 0);
    }
}
