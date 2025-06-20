using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelecting : MonoBehaviour
{
    public int sceneNumber;
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(LoadNextScene);
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
