using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demand : MonoBehaviour
{
    public Transform canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = transform.GetChild(0);
    }

    public void OnReceive()
    {
        // set this gameobject's layer to default
        this.gameObject.layer = Blackboard.defaultMask;

        // disable the canvas
        canvas.gameObject.SetActive(false);
    }

    public void ReceiveComplete(Transform package)
    {
        // parent package with this object
        package.parent = this.transform;

        // destroy this gameobject
        Destroy(this.gameObject,3f);
    }
}
