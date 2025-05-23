using TMPro;
using UnityEngine;

public class InforUIManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI hpText;

    private void Update()
    {
        SetCoinText();
        SetHpText();
    }
    public void SetCoinText()
    {
        coinText.text = CombatManager.Instance.Coin.ToString();
    }
    public void SetHpText()
    {
        hpText.text = CombatManager.Instance.Hp + "/10";
    }
}
