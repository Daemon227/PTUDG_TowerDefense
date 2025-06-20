using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelecting : MonoBehaviour
{
    public int sceneNumber;
    public string levelKey;
    public GameObject lockIcon;

    private void Start()
    {      
        gameObject.GetComponent<Button>().onClick.AddListener(LoadNextScene);
        bool isUnlocked = IsLevelUnlocked();
        gameObject.GetComponent<Button>().interactable = isUnlocked;
        if (lockIcon != null)
            lockIcon.SetActive(!isUnlocked); // hiện ổ khóa nếu chưa mở

        gameObject.GetComponent<Button>().onClick.AddListener(LoadNextScene);
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(sceneNumber);
    }
    private bool IsLevelUnlocked()
    {
        if (levelKey == "Level1")
            return true;

        return PlayerPrefs.GetInt(levelKey, 0) == 1;
    }
}
