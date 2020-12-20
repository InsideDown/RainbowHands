using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaulControlledRobotController : MonoBehaviour
{

    public Transform RightHandTransform;
    public Transform LeftHandTransform;

    private void Awake()
    {
        if (RightHandTransform == null)
            throw new System.Exception("A RightHandTransform must be defined in PaulControlledRobotController");

        if (LeftHandTransform == null)
            throw new System.Exception("A LeftHandTransform must be defined in PaulControlledRobotController");
    }

    private void Update()
    {
        EventManager.Instance.ObjectNewPosition("righthand", RightHandTransform);
        EventManager.Instance.ObjectNewPosition("lefthand", LeftHandTransform);
    }
}
