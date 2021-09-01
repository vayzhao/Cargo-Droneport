using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spot
{
    public Vector3 position;

    public Spot(Vector3 pos)
    {
        this.position = pos;
    }

    public void SetUsage(bool value)
    {
        if (value)
            Blackboard.demandSpawner.spots.Add(this);
        else
            Blackboard.demandSpawner.spots.Remove(this);
    }
}

public class DemandSpawner : MonoBehaviour
{
    [Header("Objects")]
    public GameObject matPrefab;
    public GameObject demandPrefab;
    public Transform spawnSpotParent;

    [HideInInspector]
    public List<Spot> spots;

    private float spawnInterval = 9f;
    private float checkInterval = 2f;
    private int demandRequiredCount;
    private int[] demandRequiredItemIds;

    void Start()
    {
        InitializeSpots();        
        StartCoroutine(SpawnTimer());
    }

    void InitializeSpots()
    {
        spots = new List<Spot>();
        for (int i = 0; i < spawnSpotParent.childCount; i++)
            spots.Add(new Spot(spawnSpotParent.GetChild(i).position));
    }

    IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        PreloadNextDemand();

        while (true)
        {
            if (spots.Count > demandRequiredCount)
            {
                AllocateMatAndDemand();
                yield return new WaitForSeconds(spawnInterval);
            }
            else
            {
                yield return new WaitForSeconds(checkInterval);
            }
        }
    }

    /// <summary>
    /// Method to preload the next demand
    /// </summary>
    void PreloadNextDemand()
    {
        demandRequiredCount = 1;
        demandRequiredItemIds = new int[demandRequiredCount];
        for (int i = 0; i < demandRequiredCount; i++)
        {
            demandRequiredItemIds[i] = Blackboard.itemManager.FetchARandomItemIndex();
        }
    }

    void AllocateMatAndDemand()
    {
        // spawn the demand
        var pos = spots.RandomSpot();
        var obj = Instantiate(demandPrefab, Blackboard.spawnHolder);
        var com = obj.GetComponent<Demand>();
        obj.transform.position = pos.position;
        com.Setup(demandRequiredItemIds[0], pos);

        // spawn the materials
        for (int i = 0; i < demandRequiredCount; i++)
        {
            pos = spots.RandomSpot();
            obj = Instantiate(matPrefab, Blackboard.spawnHolder);
            obj.transform.position = pos.position;
            obj.GetComponent<Mat>().Setup(demandRequiredItemIds[i], pos);
        }

        PreloadNextDemand();
    }
}