using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mat : MonoBehaviour
{
    [Tooltip("The package")]
    public Transform box;
    public Transform canvas;

    public Item item;

    // Start is called before the first frame update
    void Start()
    {
        box = transform.GetChild(0);
        canvas = transform.GetChild(1);

        Invoke("SetupIcon", Time.deltaTime);
    }

    void SetupIcon()
    {
        item = Blackboard.itemManager.FetchARandomItem();
        canvas.GetComponentInChildren<Image>().sprite = item.sprite;
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
