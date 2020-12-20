using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaulSceneController : MonoBehaviour
{

    public Text LeftHandTxt;
    public Text RightHandTxt;
    public GameObject HeadObj;
    public GameObject LeftHandObj;
    public GameObject RightHandObj;

    public GameObject RainbowHolder;
    public GameObject RainbowTrailsPrefab;

    private float _MaxDistToStartDraw = 0.25f;
    private float _DrawingTime = 1.5f;
    private float _CooldownTime = 3.0f;
    private bool _IsDrawing = false;

    private GameObject _CurLeftObj;
    private GameObject _CurRightObj;

    private void Awake()
    {
        if (LeftHandTxt == null)
            throw new System.Exception("A LeftHandTxt must be defined in PaulSceneController");

        if (RightHandTxt == null)
            throw new System.Exception("A RightHandTxt must be defined in PaulSceneController");

        if (HeadObj == null)
            throw new System.Exception("A HeadObj must be defined in PaulSceneController");

        if (LeftHandObj == null)
            throw new System.Exception("A LeftHandObj must be defined in PaulSceneController");

        if (RightHandObj == null)
            throw new System.Exception("A RightHandObj must be defined in PaulSceneController");

        if (RainbowHolder == null)
            throw new System.Exception("A RainbowHolder must be defined in PaulSceneController");

        if (RainbowTrailsPrefab == null)
            throw new System.Exception("A RainbowTrailsPrefab must be defined in PaulSceneController");

        _IsDrawing = false;
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

        if (objTypeStr == "head")
        {
            HeadObj.transform.position = objectTransform.position;
            HeadObj.transform.rotation = objectTransform.rotation;

            RightHandTxt.text = "headPos: " + objectTransform.position;
        }

        if (objTypeStr == "lefthand")
        {
            LeftHandObj.transform.position = objectTransform.position;
            LeftHandObj.transform.rotation = objectTransform.rotation;

            LeftHandTxt.text = "lefthandPos: " + objectTransform.position;

            if(_CurLeftObj != null)
            {
                _CurLeftObj.transform.position = objectTransform.position;
                _CurLeftObj.transform.rotation = objectTransform.rotation;
            }
        }

        if (objTypeStr == "righthand")
        {
            RightHandObj.transform.position = objectTransform.position;
            RightHandObj.transform.rotation = objectTransform.rotation;

            if (_CurRightObj != null)
            {
                _CurRightObj.transform.position = objectTransform.position;
                _CurRightObj.transform.rotation = objectTransform.rotation;
            }
        }
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            if(!_IsDrawing)
            {
                _IsDrawing = true;
                StartNewDrawing();
                //StartCoroutine(StartDrawing());
                CreateHands();
            }
            else
            {
                _IsDrawing = false;
                StopDrawing();
            }
            
        }
#endif

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if (!_IsDrawing)
            {
                _IsDrawing = true;
                StartNewDrawing();
                //StartCoroutine(StartDrawing());
                CreateHands();
            }
            else
            {
                _IsDrawing = false;
                StopDrawing();
            }
        }

            //if our hands are close enough, start drawing
            //float dist = Vector3.Distance(LeftHandObj.transform.position, RightHandObj.transform.position);
            //if(dist < _MaxDistToStartDraw && !_IsDrawing && HeadObj.transform.position.y < LeftHandObj.transform.position.y)
            //{
            //    StartCoroutine(StartDrawing());
            //    CreateHands();
            //}
        }

    private void CreateHands()
    {
        _CurLeftObj = Instantiate(RainbowTrailsPrefab, RainbowHolder.transform, false);
        _CurLeftObj.transform.position = LeftHandObj.transform.position;
        _CurRightObj = Instantiate(RainbowTrailsPrefab, RainbowHolder.transform, false);
        _CurRightObj.transform.position = RightHandObj.transform.position;
    }

    private IEnumerator StartDrawing()
    {
        _IsDrawing = true;
        yield return new WaitForSeconds(_DrawingTime);
        _CurLeftObj = null;
        _CurRightObj = null;
        yield return new WaitForSeconds(_CooldownTime);
        _IsDrawing = false;
    }

    private void StartNewDrawing()
    {
        _IsDrawing = true;
    }

    private void StopDrawing()
    {
        _CurLeftObj = null;
        _CurRightObj = null;
        _IsDrawing = false;
    }
}
