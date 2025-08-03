using UnityEngine;
using UnityEngine.UI;

public class ControlSetting : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public Button musicToggleButton;
    public Button sfxToggleButton;

    public GameObject musicXIcon;
    public GameObject sfxXIcon;

    private bool isMusicOn = true;
    private bool isSFXOn = true;

    void Start()
    {
        musicToggleButton.onClick.AddListener(ToggleMusic);
        sfxToggleButton.onClick.AddListener(ToggleSFX);

        UpdateUI();
    }

    void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        musicSource.mute = !isMusicOn;
        UpdateUI();
    }

    void ToggleSFX()
    {
        isSFXOn = !isSFXOn;
        sfxSource.mute = !isSFXOn;
        UpdateUI();
    }

    void UpdateUI()
    {
        musicXIcon.SetActive(!isMusicOn);
        sfxXIcon.SetActive(!isSFXOn);
    }
}
