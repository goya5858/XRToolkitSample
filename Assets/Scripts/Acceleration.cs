using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR;

public class Acceleration : MonoBehaviour
{
    [SerializeField]
    private XRNode controllerType = XRNode.RightHand; //�f�t�H���g�ł�LeftHand�ɂ���@�E��ɃA�^�b�`���ꂽ�Ƃ��͓��I�ɕύX�����
    List<XRNodeState> nodeStatesCache = new List<XRNodeState>(); //DeviceState�����ׂē��肷��

    // ���̎w���ێ����Ă���R���g���[���[
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
        // ���̃X�N���v�g���A�^�b�`����Ă���w�ɁA���E�ǂ��炩�̃R���g���[���[��ێ�������
        // InputDevices.GetDeviceAtXRNode��Awake�Ŏ��s���Ă��R���g���[���[���擾�ł��Ȃ��̂Œ���
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

