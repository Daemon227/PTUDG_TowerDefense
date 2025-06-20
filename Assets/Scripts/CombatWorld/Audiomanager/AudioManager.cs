using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip defaultClip;
    public AudioClip bossClip;

    [HideInInspector] public bool isChangeAudioClip = false;

    public static AudioManager Instance;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        audioSource.clip = defaultClip;
        audioSource.Play();
    }

    private void Update()
    {
        ChangeAudioClip();
        EndAudio();
    }
    private void ChangeAudioClip()
    {
        if (isChangeAudioClip)
        {
            audioSource.clip = bossClip;
            audioSource.Play();
            isChangeAudioClip = false;
        }
    }
    public void EndAudio()
    {
        if (CombatManager.Instance.IsGameOver)
        {
            audioSource.Pause();
        }
    }
}
