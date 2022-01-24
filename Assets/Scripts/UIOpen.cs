using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR;

public class UIOpen : MonoBehaviour
{
    [SerializeField]
    private XRNode controllerType = XRNode.RightHand; //�f�t�H���g�ł�LeftHand�ɂ���@�E��ɃA�^�b�`���ꂽ�Ƃ��͓��I�ɕύX�����
    //List<XRNodeState> nodeStatesCache = new List<XRNodeState>(); //DeviceState�����ׂē��肷��

    // ���̃I�u�W�F�N�g���ێ����Ă���R���g���[���[
    private InputDevice controller;

    InputFeatureUsage<Vector3> deviceVelocity = CommonUsages.deviceVelocity;
    Vector3 deviceVelocityValue;
    public float velocityThreshold = 0.8f;

    InputFeatureUsage<float> grip = CommonUsages.grip;
    float gripValue;
    InputFeatureUsage<bool> thumbTouch = CommonUsages.primary2DAxisTouch;
    bool thumbTouchValue;

    bool controllerAtached = false;

    public GameObject UICanvas;
    GameObject clonedUI;
    public GameObject UIPosOffset;
    public GameObject UIAngleOffset;
    bool isShowUI = false;
    public GameObject IndexFinger;
    

    private void getController()
    {
        // ���̃X�N���v�g���A�^�b�`����Ă���w�ɁA���E�ǂ��炩�̃R���g���[���[��ێ�������
        // InputDevices.GetDeviceAtXRNode��Awake�Ŏ��s���Ă��R���g���[���[���擾�ł��Ȃ��̂Œ���
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
        //if (!controllerAtached)
        //{
        //    getController();
        //    return;
        //}
        if (IndexFinger.GetComponent<TrailRenderer>().enabled)
        {
            IndexFinger.GetComponent<TrailRenderer>().enabled = false;
        }

        controller.TryGetFeatureValue(grip, out gripValue);
        controller.TryGetFeatureValue(thumbTouch, out thumbTouchValue);
        if (gripValue > 0.5f && thumbTouchValue)
        {
            //InputTracking.GetNodeStates(nodeStatesCache);
            //XRNodeState nodeState = nodeStatesCache[4];//LeftHand
            //XRNodeState nodeState = nodeStatesCache[5];//RightHand
            //Debug.Log( nodeState.TryGetAcceleration(out deviceAccelerationValue) );

            //controller.TryGetFeatureValue(deviceAcceleration, out deviceAccelerationValue);
            controller.TryGetFeatureValue(deviceVelocity, out deviceVelocityValue);
            Debug.Log(deviceVelocityValue);

            //Trail�̃I��
            IndexFinger.GetComponent<TrailRenderer>().enabled = true;
            if (deviceVelocityValue.y < -velocityThreshold && !isShowUI)
            {
                Vector3 popPos   = UIPosOffset.transform.position;
                Quaternion popRot   = UIPosOffset.transform.rotation;
                //Transform popTrans = UIPosOffset.transform;
                popPos.y += 0.3f;

                clonedUI = Instantiate(UICanvas, popPos, popRot);
                clonedUI.transform.GetChild(0).GetComponent<Canvas>().worldCamera = gameObject.GetComponent<Camera>();
                // UI��Canvas�R���|�[�l���g��WorldCamera�ɁA���̃X�N���v�g���A�^�b�`����Ă�I�u�W�F�N�g��Camera�R���|�[�l���g��n��
                clonedUI.GetComponent<FollowHUD>().target = UIAngleOffset.transform;
                popPos.y -= 0.3f;
                clonedUI.GetComponent<FollowHUD>().posTargetPos = popPos;
                // UI�̃X�N���v�g�̐ݒ�
                isShowUI = true;
            } else if (deviceVelocityValue.y > velocityThreshold && isShowUI && clonedUI)
            {
                Destroy(clonedUI);
                isShowUI = false;
            }
        }
    }
}

