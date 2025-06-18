using UnityEngine;
using UnityEngine.UI;

public class CoinGetting : MonoBehaviour
{
    public int currentGold = 0;
    public Text rewardText;     // UI hiển thị phần thưởng sau trận

    // Gọi hàm này sau khi thắng
    public void OnVictory()
    {
        int rewardGold = Random.Range(150, 251); // nhận 50-150 vàng
        currentGold += rewardGold;

        // Hiển thị thông báo phần thưởng
        rewardText.text = "+ " + rewardGold.ToString();
        rewardText.gameObject.SetActive(true);

        // Cập nhật số vàng trong UI
        UpdateGoldUI();
    }

    void UpdateGoldUI()
    {
        
    }
}