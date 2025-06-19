using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BuyTowerUIManager : MonoBehaviour
{
    private AudioSource audioSource;
    [HideInInspector] public AudioClip currentAudioClip;
    public AudioClip notEnoughtMoneyClip;
    public AudioClip sellTowerClip;

    public static BuyTowerUIManager Instance;
    private Dictionary<Vector3, GameObject> listSpawnPos = new Dictionary<Vector3, GameObject>();
    public Vector3 currentPosToSpawnTower = Vector3.zero;
    private Dictionary<Vector3, SpawnPos> listSpawnPosObjects = new Dictionary<Vector3, SpawnPos>();

    public GameObject newTower = null;

    public Dictionary<Vector3, GameObject> ListSpawnPos { get => listSpawnPos; set => listSpawnPos = value; }
    public Dictionary<Vector3, SpawnPos> ListSpawnPosObjects { get => listSpawnPosObjects; set => listSpawnPosObjects = value; }

    private void Awake()
    {
        if (Instance!= null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) gameObject.AddComponent<AudioSource>();
    }
    public GameObject GetCurrentObject()
    {
        if (!listSpawnPos.ContainsKey(currentPosToSpawnTower))
        {
            return null ;
        }
        return listSpawnPos[currentPosToSpawnTower];
    }
    public void SpawnTower(GameObject gameObject)
    {  
        if (!listSpawnPos.ContainsKey(currentPosToSpawnTower))
        {
            return;
        }
        if (listSpawnPos[currentPosToSpawnTower] == null)
        {
            GameObject tower = Instantiate(gameObject, currentPosToSpawnTower, Quaternion.identity);
            listSpawnPos[currentPosToSpawnTower] = tower;
            if (listSpawnPosObjects[currentPosToSpawnTower] != null)
            {
                listSpawnPosObjects[currentPosToSpawnTower].HideSpawnPosAfterSpawn();
            }
            currentAudioClip = tower.GetComponent<ITower>().VoiceAudioClip;
            audioSource.clip = currentAudioClip;
            audioSource.Play();
        }
    }

    public void LevelUpTower()
    {
        if (!listSpawnPos.ContainsKey(currentPosToSpawnTower))
        {
            return;
        }

        if (newTower == null)
        {
            Debug.Log("new tower null");
            return;
        }
        int amount = newTower.GetComponent<ITower>().Cost;
        if (CombatManager.Instance.CheckCost(amount))
        {
            GameObject currentTower = listSpawnPos[currentPosToSpawnTower];
            listSpawnPos[currentPosToSpawnTower] = null;
            Destroy(currentTower);
            GameObject tower = Instantiate(newTower, currentPosToSpawnTower, Quaternion.identity);
            listSpawnPos[currentPosToSpawnTower] = tower;
            CombatManager.Instance.Coin -= amount;

            CombatPanelManager.Instance.CloseAllUI();
            currentAudioClip = tower.GetComponent<ITower>().VoiceAudioClip;
            audioSource.clip = currentAudioClip;
            audioSource.Play();
        }
        else
        {
            Debug.Log("not enought money");
            audioSource.clip = notEnoughtMoneyClip;
            audioSource.Play();
        }
    }

    public void SellTower()
    {
        Debug.Log("Say sthing");
        if (!listSpawnPos.ContainsKey(currentPosToSpawnTower))
        {
            Debug.Log("Say bye bye");
            return;
        }
        Debug.Log("Say hello");
        GameObject currentTower = listSpawnPos[currentPosToSpawnTower];
        int coint = currentTower.GetComponent<ITower>().Cost;
        listSpawnPos[currentPosToSpawnTower] = null;
        Destroy(currentTower);
        CombatManager.Instance.Coin += (int)(coint * 0.6f);

        listSpawnPosObjects[currentPosToSpawnTower].ShowSpawnPosAgain();
        CombatPanelManager.Instance.CloseAllUI();

        audioSource.clip = sellTowerClip;
        audioSource.Play();
    }

    public void RemoveTower()
    {
        if (!listSpawnPos.ContainsKey(currentPosToSpawnTower))
        {
            Debug.Log("khong co");
            return;
        }
        Debug.Log("Cos");
        GameObject currentTower = listSpawnPos[currentPosToSpawnTower];
        listSpawnPos[currentPosToSpawnTower] = null;
        Destroy(currentTower);
        listSpawnPosObjects[currentPosToSpawnTower].ShowSpawnPosAgain();
    }
}
