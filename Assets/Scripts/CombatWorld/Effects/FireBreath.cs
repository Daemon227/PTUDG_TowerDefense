using UnityEngine;

public class FireBreath : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            BuyTowerUIManager.Instance.currentPosToSpawnTower = collision.transform.position;
            BuyTowerUIManager.Instance.RemoveTower();
        }
    }
}
