using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The script to manage all the drones in the scene
/// <para>Contributor: Weizhao</para>
/// </summary>
[Serializable]
public class DroneData
{
    public Drone script;
    public bool speedDebuff;
    public Vector3 position { get { return transform.position; } }
    
    private Vector3 startLoc;
    private Transform transform;    

    public DroneData(Transform refTransform)
    {
        transform = refTransform;
        startLoc = transform.position;
        script = transform.GetComponent<Drone>();
    }

    public bool IsCollidingWith(DroneData other)
    {
        return (position - other.position).sqrMagnitude <= Blackboard.DISTANCE_DRONE_COLLISION;
    }

    public Vector3 FindHitPoint(DroneData other)
    {
        return (position + other.position)/ 2f;
    }

    public void ReturnToBase()
    {
        transform.position = startLoc;
        script.Pause(Blackboard.COUNTER_REBIRTH - 1);
    }

    public bool IsWorking() { return !script.IsPaused(); }

}
public class DroneNetwork : MonoBehaviour
{
    public DroneData[] droneData;
    public GameObject collisionFx;

    void Start()
    {
        InitializeAllDrones();
    }
    void Update()
    {
        CheckDronesCollision();
    }

    void InitializeAllDrones()
    {
        // first, find all drone objects in the scene
        var drones = GameObject.FindGameObjectsWithTag("Drone");

        // initialize drone data array with length based on the quantity of drones we have found
        droneData = new DroneData[drones.Length];
        for (int i = 0; i < droneData.Length; i++)
        {
            droneData[i] = new DroneData(drones[i].transform);
        }
        
    }

    void CheckDronesCollision()
    {
        for (int i = 0; i < droneData.Length; i++)
        {
            if (droneData[i].IsWorking())
            {
                for (int j = 0; j < droneData.Length; j++)
                {
                    if (i != j && droneData[j].IsWorking() && droneData[i].IsCollidingWith(droneData[j])) 
                    {
                        DroneCollision(droneData[i], droneData[j]);
                    }
                }
            }
        }
    }
    void DroneCollision(DroneData droneA, DroneData droneB)
    {
        // find the mid point of this 2 drones
        var hitPoint = droneA.FindHitPoint(droneB);
        var explodeFx = Instantiate(collisionFx, Blackboard.spawnHolder);
        explodeFx.transform.position = hitPoint;
        Destroy(explodeFx, 3f);

        // play sound effect
        FindObjectOfType<AudioManager>().PlayBombSound();

        // return this 2 drones to the base
        droneA.ReturnToBase();
        droneB.ReturnToBase();
    }


}