using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OnLoad : MonoBehaviour
{
    // Start is called before the first frame update
    private NetworkRig obj;
    void Start()
    {

        // Perform processing here
        Debug.Log("OnLoad: Start() called.");
        obj = GetComponent<NetworkRig>();
        obj.Spawned();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
