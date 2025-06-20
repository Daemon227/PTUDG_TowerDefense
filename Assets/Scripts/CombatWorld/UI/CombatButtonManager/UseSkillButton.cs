using UnityEngine;
using UnityEngine.UI;

public class UseSkillButton : MonoBehaviour
{
    public Image loadingImg;
    public Image skillImg;
    public GameObject skillPrefab;
    public GameObject notificationPanel;
    public bool CanChangeSkill = true;

    private Button button;
    private bool isCanUseSkill = true;
    private bool isWaitingForClick = false;

    private float countdown;
    private float currentTime;

    void Start()
    {
        FirstSettup();
        if (skillPrefab == null) return;
        button = GetComponent<Button>();
        button.onClick.AddListener(StartSelectSkill);
        countdown = skillPrefab.GetComponent<ISkill>().CountDown;
        skillImg.sprite = skillPrefab.GetComponent<ISkill>().SkillSprite;
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
        if (isWaitingForClick && Input.GetMouseButtonDown(1))
        {
            CloseSkill();
        }
        HandleCooldown();
    }

    public void FirstSettup()
    {
        if (!CanChangeSkill) return;
        if (ResourceManager.Instance == null)
        {
            Debug.Log("ResourceManager.Instance is null!");
            return;
        }
        GameObject newSkill = ResourceManager.Instance.currentSkill;
        if (newSkill == null) return;
        else
        {
            skillPrefab = newSkill;
        }
    }
    public void StartSelectSkill()
    {
        if (!isCanUseSkill) return;

        isWaitingForClick = true;
        notificationPanel.SetActive(true);
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
        notificationPanel.SetActive(false);
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

    public void CloseSkill()
    {
        if (isWaitingForClick)
        {
            isWaitingForClick = false;
            notificationPanel.SetActive(false);
        }      
    }
}
