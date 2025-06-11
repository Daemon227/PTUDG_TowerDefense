using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    public Image icon;
    public Text quantityText;
    public string description;
    public Text descriptionText;

    private int quantity = 0;

    void Start()
    {
        UpdateUI();
    }

    public void UpdateQuantity(int newQuantity)
    {
        quantity = newQuantity;
        UpdateUI();
    }

    public void AddItem(int amount)
    {
        quantity += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        quantityText.text = quantity.ToString();
        quantityText.gameObject.SetActive(quantity > 0);  // Ẩn nếu = 0
        icon.enabled = quantity > 0;                      // Ẩn icon nếu chưa có
    }

    public void OnItemClick()
    {
        descriptionText.text = description; // Ghi đè mô tả
    }
}
