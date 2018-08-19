using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {
    public Transform target;
    private Vector3 offest = new Vector3(0,9,-12);
    private float smoothing = 2;
	void Update () {
        Vector3 targetPos = target.position + offest;
        transform.position = Vector3.Lerp(transform.position,targetPos,smoothing*Time.deltaTime);
        transform.LookAt(target);
	}
}
