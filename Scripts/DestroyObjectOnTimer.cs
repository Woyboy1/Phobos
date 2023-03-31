using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectOnTimer : MonoBehaviour
{
    [SerializeField] private float timer = 3.0f;

    private void Start()
    {
        Destroy(this.gameObject, timer);
    }
}
