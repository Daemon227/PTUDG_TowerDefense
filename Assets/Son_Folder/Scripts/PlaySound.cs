using UnityEngine;
using UnityEngine.UI;

public class PlaySound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip soundClip;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(PlayAudio);
    }

    void PlayAudio()
    {
        if (audioSource != null && soundClip != null)
        {
            audioSource.PlayOneShot(soundClip);
        }
    }
}
