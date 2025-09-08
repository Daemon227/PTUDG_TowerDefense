using UnityEngine;
using UnityEngine.UI;

public class EHpBar : MonoBehaviour
{
    private float maxHp;
    private float currentHp;
    public Image hpbar;

    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.rotation; // lưu rotation ban đầu
    }

    void LateUpdate()
    {
        transform.rotation = initialRotation; // giữ nguyên rotation
    }
    public void SetHpInfor(float hp)
    {
        maxHp = hp;
        currentHp = hp;
        hpbar.fillAmount = 1;
    }

    public void FillHpBar(float currentHp)
    {
        hpbar.fillAmount = currentHp / maxHp;
    }
}
