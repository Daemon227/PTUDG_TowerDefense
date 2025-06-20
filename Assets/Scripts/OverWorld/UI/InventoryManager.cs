using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<Button> listButton; // Gán trong Inspector
    public List<Image> images;
    //public List<Image> skillimage;
    public Text description;
    public Button confirmButton;

    private int currentOption = -1;
    private bool isSettup = false;
    private void Start()
    {
        listButton[0].onClick.AddListener(Clicked1);
        listButton[1].onClick.AddListener(Clicked2);
        confirmButton.onClick.AddListener(ConfirmClicked);
        
        FirstSettup();
    }
    private void Update()
    {
        SettupSkill();
    }
    public void FirstSettup()
    {
        if (ResourceManager.Instance.skillLists.Count <= 0)
        {
            for (int i = 0; i < listButton.Count; i++)
            {
                listButton[i].gameObject.SetActive(false);
            }
        }
        else
        {
            currentOption = 0;
        }
    }
    public void SettupSkill()
    {
        int skills = ResourceManager.Instance.skillLists.Count;
        if (skills <= 0) return;
        for (int i = 0; i< listButton.Count; i++)
        {
            if (i < skills)
            {
                listButton[i].gameObject.SetActive(true);
                ISkill s = ResourceManager.Instance.skillLists[i].GetComponent<ISkill>();
                images[i].sprite = s.SkillSprite;

                if(i == currentOption)
                {
                    description.text = s.Description;
                }
            }
        }
    }

    public void Clicked1()
    {
        currentOption = 0;
        
    }
    public void Clicked2()
    {
        currentOption = 1;
    }

    public void ConfirmClicked()
    {
        if((currentOption < ResourceManager.Instance.skillLists.Count) && (currentOption >= 0))
        {
            ResourceManager.Instance.currentSkill = ResourceManager.Instance.skillLists[currentOption];
        }
    }

}
