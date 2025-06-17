using UnityEngine;

public class AdditionEffect : MonoBehaviour
{
    public float delay = 1f;
    private void Start()
    {
        Destroy(gameObject, delay);
    }
}
