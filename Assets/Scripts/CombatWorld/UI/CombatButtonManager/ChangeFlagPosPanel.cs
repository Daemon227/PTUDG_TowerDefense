using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeFlagPosPanel : MonoBehaviour
{
    private Button button;
    private ChangeFlagPos changeFlagPos;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnChangeFlagPosClick);
    }

    private void Start()
    {
        changeFlagPos = GetComponentInParent<ChangeFlagPos>();
        if (changeFlagPos == null)
        {
            Debug.Log("Chang flag pos is null");
            return;
        }
    }

    public void OnChangeFlagPosClick()
    {
        Debug.Log("Chọn di chuyển cờ");
        CombatPanelManager.Instance.CloseAllUI();
        changeFlagPos.canChange = true;

        this.gameObject.SetActive(false);
    }
}
