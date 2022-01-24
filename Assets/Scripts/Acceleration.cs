using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR;

public class Acceleration : MonoBehaviour
{
    [SerializeField]
    private XRNode controllerType = XRNode.RightHand; //デフォルトではLeftHandにする　右手にアタッチされたときは動的に変更される
    List<XRNodeState> nodeStatesCache = new List<XRNodeState>(); //DeviceStateをすべて入手する

    // この指が保持しているコントローラー
    private InputDevice controller;

    InputFeatureUsage<Vector3> deviceAcceleration = CommonUsages.deviceAngularAcceleration;
    Vector3 deviceAccelerationValue;

    InputFeatureUsage<Vector3> deviceVelocity = CommonUsages.deviceVelocity;
    Vector3 deviceVelocityValue;

    InputFeatureUsage<bool> primarybutton = CommonUsages.primaryButton;
    bool primarybuttonValue = false;

    bool controllerAtached = false;

    private void getController()
    {
        // このスクリプトがアタッチされている指に、左右どちらかのコントローラーを保持させる
        // InputDevices.GetDeviceAtXRNodeはAwakeで実行してもコントローラーを取得できないので注意
        if (true)//(controllerType == XRNode.LeftHand || controllerType == XRNode.RightHand)
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

    private void Start() {}

    private void Update()
    {
        if (!controllerAtached)
        {
            getController();
            return;
        }

        //Debug.Log( controller.TryGetFeatureValue(deviceAcceleration, out deviceAccelerationValue) );
        //controller.TryGetVelocity();
        //Debug.Log(deviceAccelerationValue);
        controller.TryGetFeatureValue(primarybutton, out primarybuttonValue);
        if (primarybuttonValue)
        {
            //InputTracking.GetNodeStates(nodeStatesCache);
            ////XRNodeState nodeState = nodeStatesCache[4];//LeftHand
            //XRNodeState nodeState = nodeStatesCache[5];//RightHand
            //Debug.Log( nodeState.TryGetAcceleration(out deviceAccelerationValue) );
            //Debug.Log(deviceAccelerationValue);

            controller.TryGetFeatureValue(deviceAcceleration, out deviceAccelerationValue);
            //Debug.Log(deviceAccelerationValue);
            controller.TryGetFeatureValue(deviceVelocity, out deviceVelocityValue);
            Debug.Log(deviceVelocityValue);
        }
    }
}

