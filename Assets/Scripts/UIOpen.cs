using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR;

public class UIOpen : MonoBehaviour
{
    [SerializeField]
    private XRNode controllerType = XRNode.RightHand; //デフォルトではRightHandにする

    // このオブジェクトが保持しているコントローラー
    private InputDevice controller;
    bool controllerAtached = false;

    // コントローラーの速度関係の変数
    InputFeatureUsage<Vector3> deviceVelocity = CommonUsages.deviceVelocity;
    Vector3 deviceVelocityValue;
    public float velocityThreshold = 0.8f;

    // コントローラーのボタン関係の変数
    InputFeatureUsage<float> grip = CommonUsages.grip;
    float gripValue;
    InputFeatureUsage<bool> thumbTouch = CommonUsages.primary2DAxisTouch;
    bool thumbTouchValue;

    public  GameObject UICanvas;
    private GameObject clonedUI;

    public  GameObject UIPosOffset;
    public  GameObject UIAngleOffset;
    bool    isShowUI = false;

    //public  GameObject IndexFinger;
    

    private void getController()
    {
        // このスクリプトがアタッチされている指に、左右どちらかのコントローラーを保持させる
        // InputDevices.GetDeviceAtXRNodeはAwakeで実行してもコントローラーを取得できないので注意
        if (controllerType == XRNode.LeftHand || controllerType == XRNode.RightHand)
        {
            controller = InputDevices.GetDeviceAtXRNode(controllerType);
            if (controller.isValid)
            {
                controllerAtached = true;
            }
            Debug.Log(controllerType);
            Debug.Log(controller.name);
            Debug.Log(controller.isValid);
        }
    }

    private void Start() {
        if (!controllerAtached)
        {
            getController();
            return;
        }
    }

    private void Update()
    {
        if (!controllerAtached)
        {
            getController();
            return;
        }

        //if (IndexFinger.GetComponent<TrailRenderer>().enabled)
        //{
        //    IndexFinger.GetComponent<TrailRenderer>().enabled = false;
        //}

        controller.TryGetFeatureValue(grip, out gripValue);
        controller.TryGetFeatureValue(thumbTouch, out thumbTouchValue);
        if (gripValue > 0.5f && thumbTouchValue)
        {
            controller.TryGetFeatureValue(deviceVelocity, out deviceVelocityValue);

            //Trailのオン
            //IndexFinger.GetComponent<TrailRenderer>().enabled = true;
            if (deviceVelocityValue.y < -velocityThreshold && !isShowUI)
            {
                Vector3    popPos   = UIPosOffset.transform.position;
                Quaternion popRot   = UIPosOffset.transform.rotation;
                //Transform popTrans = UIPosOffset.transform;
                popPos.y += 0.3f;

                clonedUI = Instantiate(UICanvas, popPos, popRot);
                clonedUI.transform.GetChild(0).GetComponent<Canvas>().worldCamera = gameObject.GetComponent<Camera>();
                // UIのCanvasコンポーネントのWorldCameraに、このスクリプトがアタッチされてるオブジェクトのCameraコンポーネントを渡す
                clonedUI.GetComponent<FollowHUD>().target = UIAngleOffset.transform;
                popPos.y -= 0.3f;
                clonedUI.GetComponent<FollowHUD>().posTargetPos = popPos;
                // UIのスクリプトの設定
                isShowUI = true;
            } else if (deviceVelocityValue.y > velocityThreshold && isShowUI && clonedUI)
            {
                Destroy(clonedUI);
                isShowUI = false;
            }
        }
    }
}

