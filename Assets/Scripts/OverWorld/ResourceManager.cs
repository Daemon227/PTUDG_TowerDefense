using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int ruby;
    public List<GameObject> boughtSkillList = new List<GameObject>();
    public GameObject currentSkill;

    public List<GameObject> allSkillList = new List<GameObject>();
    public static ResourceManager Instance;
    private void Awake()
    {
        
        DontDestroyOnLoad(gameObject);

        if (PlayerPrefs.HasKey("PlayerRuby"))
        {
            ruby = PlayerPrefs.GetInt("PlayerRuby");
        }
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

       LoadGame();
    }

    public void SetSkill(int index)
    {
        currentSkill = boughtSkillList[index];
    }

    public void SaveGame()
    {

        SaveData data = new SaveData
        {
            ruby = this.ruby,
            boughtSkillInID = new List<int>(),
            currentSkillInID = boughtSkillList.IndexOf(currentSkill)
        };


        for (int i = 0; i < boughtSkillList.Count; i++)
        {
            data.boughtSkillInID.Add(boughtSkillList[i].GetComponent<ISkill>().GetID);
        }
        string json = JsonUtility.ToJson(data);   
        string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/MyGame";

        if (!System.IO.Directory.Exists(folderPath))
        {
            System.IO.Directory.CreateDirectory(folderPath);
        }

        string filePath = folderPath + "/save.json";
        System.IO.File.WriteAllText(filePath, json);
    }

    public void LoadGame()
    {
        string filePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/MyGame/save.json";
        try
        {
            if (System.IO.File.Exists(filePath))
            {
                string json = System.IO.File.ReadAllText(filePath);
                SaveData data = JsonUtility.FromJson<SaveData>(json);

                this.ruby = data.ruby;
                //set lai danh sach skill da mua
                for (int i = 0; i < data.boughtSkillInID.Count; i++)
                {
                    for (int j = 0; j < allSkillList.Count; j++)
                    {
                        ISkill skill = allSkillList[j].GetComponent<ISkill>();
                        if (skill.GetID == data.boughtSkillInID[i])
                        {
                            boughtSkillList.Add(allSkillList[j]);
                        }
                    }
                }
                //set skill dang su dung.
                foreach (var s in allSkillList)
                {
                    if (data.currentSkillInID == s.GetComponent<ISkill>().GetID)
                    {
                        currentSkill = s;
                        return;
                    }
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Lỗi khi load save file: " + ex.Message);
        } 
    }
}

[System.Serializable]
public class SaveData
{
    public int ruby;
    public List<int> boughtSkillInID;
    public int currentSkillInID;
}
