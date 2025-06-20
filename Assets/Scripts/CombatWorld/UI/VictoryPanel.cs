using System.Collections;
using TMPro;
using UnityEngine;

public class VictoryPanel : MonoBehaviour
{
    public TextMeshProUGUI rubyText;
    public float delayTime = 0.1f;
    public int rubyCanRecive;

    private void Start()
    {
        StartCoroutine(RunRuby());
    }
    public IEnumerator RunRuby()
    {
        for (int i = 1; i <= rubyCanRecive; i++)
        {
            rubyText.text = "+" + i;
            yield return new WaitForSeconds(delayTime); 
        }
        if (ResourceManager.Instance != null)
        {
            ResourceManager.Instance.ruby += rubyCanRecive;
            ResourceManager.Instance.SaveGame();
        }
        Time.timeScale = 0;
    }
}
