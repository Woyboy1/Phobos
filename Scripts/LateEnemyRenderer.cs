using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateEnemyRenderer : MonoBehaviour
{
    [SerializeField] private GameObject[] monsterPrefabs;
    [SerializeField] private float initializeTimer; [Tooltip("How long before the monsters are set active")]

    private void Start()
    {
        foreach (GameObject monster in monsterPrefabs)
        {
            monster.SetActive(false);
        }

        StartCoroutine(InitializeMonsters());
    }

    IEnumerator InitializeMonsters()
    {
        yield return new WaitForSeconds(initializeTimer);
        foreach (GameObject monster in monsterPrefabs)
        {
            monster.SetActive(true);
        }
    }
}
