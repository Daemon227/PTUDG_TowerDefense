using UnityEngine;
using UnityEngine.UI;

public class UseSkillButton : MonoBehaviour
{
    public Image loadingImg;
    public GameObject skillPrefab;

    private Button button;
    private bool isCanUseSkill = true;
    private bool isWaitingForClick = false;

    private float countdown;
    private float currentTime;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(StartSelectSkill);

        countdown = skillPrefab.GetComponent<ISkill>().CountDown;
        currentTime = 0;
        loadingImg.fillAmount = 0;
        loadingImg.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isWaitingForClick && Input.GetMouseButtonDown(0))
        {
            CastSkillAtMousePosition();
        }

        HandleCooldown();
    }

    public void StartSelectSkill()
    {
        if (!isCanUseSkill) return;

        isWaitingForClick = true;
        Debug.Log("Chờ chọn vị trí để dùng skill...");
    }

    private void CastSkillAtMousePosition()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Instantiate(skillPrefab, mousePos, Quaternion.identity);

        isCanUseSkill = false;
        isWaitingForClick = false;
        currentTime = countdown;

        loadingImg.fillAmount = 1;
        loadingImg.gameObject.SetActive(true);
    }

    private void HandleCooldown()
    {
        if (isCanUseSkill) return;

        currentTime -= Time.deltaTime;
        loadingImg.fillAmount = currentTime / countdown;

        if (currentTime <= 0)
        {
            isCanUseSkill = true;
            loadingImg.fillAmount = 0;
            loadingImg.gameObject.SetActive(false);
        }
    }
}
