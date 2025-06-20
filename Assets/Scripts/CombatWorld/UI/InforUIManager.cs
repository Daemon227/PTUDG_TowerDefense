using TMPro;
using UnityEngine;

public class InforUIManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI hpText;
    private int maxHp = 0;
    private void Start()
    {
        maxHp = CombatManager.Instance.Hp;
    }
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
        hpText.text = CombatManager.Instance.Hp + "/" + maxHp;
    }
}
