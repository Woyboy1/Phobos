using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    /// <summary>
    /// Basic class for the interactable class, more methods to come in the future.
    /// 
    /// </summary>

    protected string interactableID { get; set; }
    [SerializeField] protected ParticleSystem particleFX;


    public string InteractableID
    {
        get { return interactableID; }
    }

    protected virtual void Start()
    {
        int layerElement = LayerMask.NameToLayer("Interactable");
        gameObject.layer = layerElement;
    }

    protected virtual void Update()
    {

    }

    protected virtual void InstantiateParticles()
    {
        Instantiate(particleFX, gameObject.transform.position, Quaternion.identity);
    }
}
