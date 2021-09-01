using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The manager to handle game flow
/// <para>Contributor: Weizhao</para>
/// </summary>
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Blackboard.Initialize();
    }

}
