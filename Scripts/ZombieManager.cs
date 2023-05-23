using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private Transform[] spawnLocation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);

            int randomIndex = Random.Range(0, spawnLocation.Length);

            Instantiate(zombiePrefab, spawnLocation[randomIndex].position, Quaternion.identity);
        }
    }
}
