using UnityEngine;

public interface ITower
{
    public int Cost { get; }
    public float Radius { get; }
    public int Dame { get; }
    public GameObject NewTower { get; }
    public bool CanChangePos { get; }
    public void DetectTarget();
    public void Attack(GameObject target);
    public AudioClip VoiceAudioClip { get; }
    public string GetDescription { get; }
}
