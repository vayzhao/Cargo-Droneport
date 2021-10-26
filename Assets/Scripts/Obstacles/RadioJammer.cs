using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter
{
    public bool isWorking;
    private Transform model;
    private Transform wave;
    private Material modelMat;
    private Material waveMat;
    private float durationRemain;

    public Emitter(Transform emitter)
    {
        // find the wave object of the emitter
        wave = emitter.GetChild(0);        
        waveMat = wave.GetComponent<Renderer>().material;

        // find the model object of the emitter
        model = emitter.GetChild(1);
        //modelMat = model.GetComponent<Renderer>().material;
    }

    public bool IsInRange(Vector3 pos, float distance)
    {
        Debug.Log((wave.position - pos).sqrMagnitude);
        return (wave.position - pos).sqrMagnitude <= distance * distance;
    }

    public void SetState(bool value, float duration)
    {
        isWorking = value;
        durationRemain += duration;
        wave.gameObject.SetActive(value);
        //modelMat.color = value ? Color.red : Color.green;
    }

    public void UpdateWaveOffset()
    {
        var offset = waveMat.mainTextureOffset;
        offset.y += 1f*Time.deltaTime;
        waveMat.mainTextureOffset = offset;

        durationRemain -= Time.deltaTime;
        if (durationRemain <= 0f)
        {
            SetState(false, 0f);
        }
    }
}

public class RadioJammer : MonoBehaviour
{
    public float radius = 50f;
    public float speedDecreaseRate = 0.9f;
    public float triggerInterval = 2f;
    public float effectiveDuration = 1f;

    private float timer;

    private Emitter[] emitters;
    private DroneNetwork drones;


    // Start is called before the first frame update
    void Start()
    {
        drones = FindObjectOfType<DroneNetwork>();

        SetupEmitters();
    }

    void SetupEmitters()
    {
        var count = transform.childCount;
        emitters = new Emitter[count];
        for (int i = 0; i < count; i++) { 
            emitters[i] = new Emitter(transform.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        EmitterCheck();
        RadioWave();
           
    }

    void EmitterCheck()
    {
        timer += Time.deltaTime;
        if (timer >= triggerInterval)
        {
            var index = UnityEngine.Random.Range(0, emitters.Length);
            emitters[index].SetState(true, effectiveDuration);
            timer = 0f;
        }
    }

    void RadioWave()
    {
        foreach (var drone in drones.droneData)
        {
            if (!drone.script.IsPaused())
            {
                var isInRange = false;
                foreach (var emitter in emitters)
                {
                    if (emitter.isWorking && emitter.IsInRange(drone.position, radius))
                    {
                        isInRange = true;
                        break;
                    }
                }

                if (isInRange && !drone.speedDebuff)
                {
                    drone.speedDebuff = true;
                    drone.script.ChangeSpeed(1 - speedDecreaseRate);
                }
                else if (!isInRange && drone.speedDebuff)
                {
                    drone.speedDebuff = false;
                    drone.script.ChangeSpeed(1f);
                }
            }

            
        }

        foreach (var emitter in emitters)
        {
            if (emitter.isWorking)
            {
                emitter.UpdateWaveOffset();
            }

        }
    }
}
