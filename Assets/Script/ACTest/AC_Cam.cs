using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_Cam : MonoBehaviour
{
    public Transform drone;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = drone.position - this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = drone.position - offset;
    }
}
