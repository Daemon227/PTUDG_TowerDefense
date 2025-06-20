using UnityEngine;
using UnityEngine.UI;

public class ShopSkillManager : MonoBehaviour
{
    public Text rubyText;
    public ItemShopData item1;
    public ItemShopData item2;
    void Start()
    {
        item1.SetupButton();
        item2.SetupButton();
    }

    public void Update()
    {
        rubyText.text = ResourceManager.Instance.ruby + "";
        CheckBought(item1);
        CheckBought(item2);
    }

    public void CheckBought(ItemShopData item)
    {
        if (ResourceManager.Instance.skillLists.Contains(item.skillObject))
        {
            item.lockPanel.SetActive(false);
            item.costText.text = "Đã mua";
        }
        else
        {
            item.SetText();
        }

    }
    
}

[System.Serializable]
public class ItemShopData
{
    public GameObject skillObject;
    public Button button;
    public Text costText;
    public GameObject lockPanel;

    public void SetText()
    {
        ISkill skill = skillObject.GetComponent<ISkill>();

        if (skill == null)
        {
            Debug.Log("skill null");
            return;
        }
        costText.text = skill.Cost + "";
    }

    public void SetupButton()
    {
        button.onClick.AddListener(() =>
        {
            BuySkill();
        });
    }
    public void BuySkill()
    {
        ISkill skill = skillObject.GetComponent<ISkill>();
        if (skill == null) return;

        int cost = skill.Cost;

        if (ResourceManager.Instance.ruby >= cost && !ResourceManager.Instance.skillLists.Contains(skillObject))
        {
            ResourceManager.Instance.ruby -= cost;
            ResourceManager.Instance.skillLists.Add(skillObject);
            lockPanel.SetActive(false);
            costText.text = "Đã mua";
        }
        else
        {
            Debug.Log("Không đủ tiền hoặc đã mua rồi");
        }
    }


}
