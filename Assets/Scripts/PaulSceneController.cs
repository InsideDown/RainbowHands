using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaulSceneController : MonoBehaviour
{

    public Text LeftHandTxt;
    public Text RightHandTxt;
    public GameObject LeftHandObj;
    public GameObject RightHandObj;

    private void Awake()
    {
        if (LeftHandTxt == null)
            throw new System.Exception("A LeftHandTxt must be defined in PaulSceneController");

        if (RightHandTxt == null)
            throw new System.Exception("A RightHandTxt must be defined in PaulSceneController");

        if (LeftHandObj == null)
            throw new System.Exception("A LeftHandObj must be defined in PaulSceneController");

        if (RightHandObj == null)
            throw new System.Exception("A RightHandObj must be defined in PaulSceneController");
    }

    private void OnEnable()
    {
        EventManager.OnObjectNewPosition += EventManager_OnObjectNewPosition;
    }

    private void OnDisable()
    {
        EventManager.OnObjectNewPosition -= EventManager_OnObjectNewPosition;
    }

    private void EventManager_OnObjectNewPosition(string objTypeStr, Transform objectTransform)
    {   
        TrackObject(objTypeStr, objectTransform);
    }

    private void TrackObject(string objTypeStr, Transform objectTransform)
    {
        string objTrackStr = "type: " + objTypeStr + ", trans: " + objectTransform;
        if (objTypeStr == "lefthand")
        {
            LeftHandTxt.text = objTrackStr;
            LeftHandObj.transform.position = objectTransform.position;
            LeftHandObj.transform.rotation = objectTransform.rotation;
        }

        if (objTypeStr == "righthand")
        {
            RightHandTxt.text = objTrackStr;
            RightHandObj.transform.position = objectTransform.position;
            RightHandObj.transform.rotation = objectTransform.rotation;
        }
    }
}
