using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : Interactable
{
    [SerializeField] private float itemDiscoveryPercentage = 25.0f;
    [SerializeField] private int maxItemNum = 3;


    private string[] itemTypes = { "itemMetal", "itemTrash", "itemPlastic" };
    private string item;
    private int itemNum;
    private bool hasItem;

    public int ItemNum
    {
        get { return itemNum; }
    }

    public float ItemDiscoveryPercentage
    {
        get { return itemDiscoveryPercentage; }
    }

    public string Item
    {
        get { return item; }
    }

    new void Start()
    {
        base.Start();
        interactableID = "Container";
        item = itemTypes[Random.Range(0, itemTypes.Length)];
        itemNum = Random.Range(1, maxItemNum);
        this.gameObject.layer = LayerMask.NameToLayer("Interactable");
    }


    new void Update()
    {
        base.Update();
    }

    public void OpenContainer()
    {
        if (hasItem == true)
        {
            InstantiateParticles();
            AudioManager.instance.Play("ContainerOpen");

            hasItem = false;
        }

    }

    public bool HasItem()
    {
        float randomNum = Random.Range(0, 100);

        if (randomNum > itemDiscoveryPercentage)
        {
            hasItem = false;
        }
        else
        {
            hasItem = true;
        }

        return hasItem;
    }
}
