using UnityEngine;

public class MyTool : MonoBehaviour
{
    public static MyTool Instance;
    private float currentTime = 0;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public bool SetDeLayTime(float delayTime)
    {
        if (currentTime > delayTime)
        {
            currentTime = 0;
            return true;
        }
        else return false;
    }

}
