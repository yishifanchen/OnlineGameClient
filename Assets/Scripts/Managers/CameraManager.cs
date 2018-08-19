using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : BaseManager {
    private GameObject cameraGo;
    private FollowTarget followTarget;
    private Vector3 originalPosition;
    private Vector3 originalRotation;
    public CameraManager(GameFacade facade) : base(facade) { }
    public override void OnInit()
    {
        cameraGo = Camera.main.gameObject;
        followTarget = cameraGo.GetComponent<FollowTarget>();
    }
    public void FollowRole()
    {
        followTarget.target = facade.GetCurrentRoleGameObject().transform;
        originalPosition = cameraGo.transform.position;
        originalRotation = cameraGo.transform.localEulerAngles;
        //followTarget.enabled = true;
        //Quaternion targetQuaternion=Quaternion.
    }
}
