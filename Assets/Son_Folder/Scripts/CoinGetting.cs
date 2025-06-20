using UnityEngine;
using UnityEngine.UI;

public class CoinGetting : MonoBehaviour
{
    public int currentGold = 0;
    public Text rewardText;

    public void OnVictory()
    {
        int rewardGold = Random.Range(150, 251);
        currentGold += rewardGold;

        rewardText.text = "+ " + rewardGold.ToString();
        rewardText.gameObject.SetActive(true);

        UpdateGoldUI();
    }

    void UpdateGoldUI()
    {
        
    }
}