using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkRig : NetworkBehaviour
{

    private GameObject playerObj;
    private GameObject headObj;
    private GameObject leftHandObj;
    private GameObject rightHandObj;



    public bool IsLocalNetworkRig => Object.HasStateAuthority;
    [Header("RigComponents")]
    [SerializeField]
    //private NetworkTransform playerTransform = GameObject.FindWithTag("PlayerPrefab").GetComponents<NetworkTransform>()[0];
    private NetworkTransform playerTransform;


    [SerializeField]
    private NetworkTransform headTransform;//GameObject.FindWithTag("Head").GetComponents<NetworkTransform>()[0];

    [SerializeField]
    private NetworkTransform leftHandTransform; // GameObject.FindWithTag("LeftHand").GetComponents<NetworkTransform>()[0];

    [SerializeField]
    private NetworkTransform rightHandTransform; //= GameObject.FindWithTag("RightHand").GetComponents<NetworkTransform>()[0];

    HardwareRig hardwareRig;

    public override void Spawned()
    {

        /*
        DEBUG CODE
        Debug.Log("Starting NetworkRig Script");
        
        playerObj = GameObject.FindWithTag("PlayerPrefab");
        leftHandObj = GameObject.FindWithTag("LeftHand");
        rightHandObj = GameObject.FindWithTag("RightHand");



        Debug.Log(playerObj.transform.position);
        Debug.Log(leftHandObj.transform.position);
        Debug.Log(rightHandObj.transform.position);*/


    

        if (IsLocalNetworkRig)
        {
            hardwareRig = FindObjectOfType<HardwareRig>();
            if (hardwareRig == null)
                Debug.LogError("Missing HardwareRig in the scene");
        }
        // else it means that this is a client
    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        if (GetInput<RigState>(out var input))
        {
            playerTransform.transform.SetPositionAndRotation(input.PlayerPosition, input.PlayerRotation);

            headTransform.transform.SetPositionAndRotation(input.HeadsetPosition, input.HeadsetRotation);

            leftHandTransform.transform.SetPositionAndRotation(input.LeftHandPosition, input.LeftHandRotation);

            rightHandTransform.transform.SetPositionAndRotation(input.RightHandPosition, input.RightHandRotation);

        }
    }

    public override void Render()
    {
        base.Render();
        if (IsLocalNetworkRig)
        {
            playerTransform.transform.SetPositionAndRotation(hardwareRig.playerTransform.position, hardwareRig.playerTransform.rotation);

            headTransform.transform.SetPositionAndRotation(hardwareRig.headTransform.position, hardwareRig.headTransform.rotation);

            leftHandTransform.transform.SetPositionAndRotation(hardwareRig.leftHandTransform.position, hardwareRig.leftHandTransform.rotation);

            rightHandTransform.transform.SetPositionAndRotation(hardwareRig.rightHandTransform.position, hardwareRig.rightHandTransform.rotation);

        }
    }
}
