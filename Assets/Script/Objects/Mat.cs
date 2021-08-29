using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mat : MonoBehaviour
{
    [Tooltip("The package")]
    public Transform box;
    public Transform canvas;

    // Start is called before the first frame update
    void Start()
    {
        box = transform.GetChild(0);
        canvas = transform.GetChild(1);
    }

    public void OnReceive()
    {
        // set this gameobject's layer to default
        this.gameObject.layer = Blackboard.defaultMask;

        // disable the canvas
        canvas.gameObject.SetActive(false);
    }

    public void ReceiveComplete()
    {
        // destroy this gameobject
        Destroy(this.gameObject);
    }

}
