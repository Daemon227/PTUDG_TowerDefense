using System.Collections;
using UnityEngine;

public class MinotaurBoomEffect : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(EndGame());
    }

    public IEnumerator EndGame()
    {
        yield return new WaitForSeconds(0.8f);
        CombatManager.Instance.IsGameOver = true;
    }
}
