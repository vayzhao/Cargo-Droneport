using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{
    public Transform fxCanvas;
    public GameObject matIconPrefab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject CreateImage()
    {
        return Instantiate(matIconPrefab, fxCanvas);
    }

}
