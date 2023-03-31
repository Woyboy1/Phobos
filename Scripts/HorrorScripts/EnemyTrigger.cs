using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private bool spawnRandom = true;
    [Tooltip("The Spawn Custom Location will only work if spawnRandom is false")]
    [SerializeField] private Transform spawnCustomLocation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (spawnRandom)
            {
                AudioManager.instance.Play("ClownSpawn");
                Destroy(this.gameObject);
            } else
            {
                // GameHandling.instance.SpawnEnemyAt(spawnCustomLocation);
                AudioManager.instance.Play("ClownSpawn");
                Destroy(this.gameObject);
            }
        }
    }
}
