using UnityEngine;

public class Active : MonoBehaviour
{
    // Kéo các GameObject vào từ Unity Editor
    public GameObject musicXIcon;
    public GameObject sfxXIcon;

    void Start()
    {
        // Ẩn hai icon khi bắt đầu game
        musicXIcon.SetActive(false);
        sfxXIcon.SetActive(false);
    }
}
