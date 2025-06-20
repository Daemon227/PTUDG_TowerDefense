using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int ruby;

    public List<GameObject> skillLists = new List<GameObject>();
    public GameObject currentSkill;

    public static ResourceManager Instance;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if(Instance!= null && Instance!= this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SetSkill(int index)
    {
        currentSkill = skillLists[index];
    }

}
