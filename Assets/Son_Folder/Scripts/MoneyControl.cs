using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Text coinsText;
    public int armorPrice = 199;
    public int arrowPrice = 299;

    public InventoryItemUI armorItemUI;
    public InventoryItemUI arrowItemUI;

    public void BuyArmor()
    {
        TryBuyItem(armorPrice, armorItemUI);
    }

    public void BuyArrow()
    {
        TryBuyItem(arrowPrice, arrowItemUI);
    }

    void TryBuyItem(int price, InventoryItemUI item)
    {
        int coins = int.Parse(coinsText.text);
        if (coins >= price)
        {
            coins -= price;
            coinsText.text = coins.ToString();
            item.AddItem(1);
        }
        else
        {
            Debug.Log("Không đủ tiền");
        }
    }
}
