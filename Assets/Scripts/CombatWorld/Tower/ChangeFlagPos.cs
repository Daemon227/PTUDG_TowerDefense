using Unity.VisualScripting;
using UnityEngine;

public class ChangeFlagPos : MonoBehaviour
{
    [HideInInspector] public GameObject flag;
    public GameObject circle;
    [HideInInspector] public float radius;

    public bool canChange = false;

    private KnightTower knightTower;

    private void Awake()
    {
        knightTower = GetComponent<KnightTower>();
        if (knightTower == null) return;
        flag = knightTower.flag;
        radius = knightTower.radius;
    }

    private void Update()
    {
        ChangePos();
    }

    public void ChangePos()
    {
        if (!canChange) return;
        flag.SetActive(true);
        circle.SetActive(true);
        if (Input.GetMouseButton(0))
        {
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousepos.z = 0;
            Debug.Log(mousepos);
            if (Vector3.Distance(transform.position, mousepos) > radius)
            {
                Debug.Log("Vi tri qua xaa");         
                return;
            }
            else
            {
                flag.transform.position = mousepos;
            }
            circle.SetActive(false);
            flag.SetActive(false);
            canChange = false;
        }
    }
}
