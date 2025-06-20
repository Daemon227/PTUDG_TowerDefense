using UnityEngine;

public class Active : MonoBehaviour
{
    public GameObject musicXIcon;
    public GameObject sfxXIcon;

    void Start()
    {
        musicXIcon.SetActive(false);
        sfxXIcon.SetActive(false);
    }
}
