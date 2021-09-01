using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item {
    public int itemId;
    public Sprite sprite;

}

public class ItemManager : MonoBehaviour
{
    public Item[] allItems;

    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Method to return a random index from 0 to the length of allItems
    /// </summary>
    /// <returns></returns>
    public int FetchARandomItemIndex() { return UnityEngine.Random.Range(0, allItems.Length); }

    public Item FetchARandomItem()
    {
        return allItems[FetchARandomItemIndex()];
    }

}
