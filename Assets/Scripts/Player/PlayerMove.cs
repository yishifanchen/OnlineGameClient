﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    public float forward = 0;
    private float speed = 3;
    private Animator anim;
    public MouseLook mouseLook=new MouseLook();
	void Start () {
        anim = GetComponent<Animator>();
        mouseLook.Init(transform,Camera.main.transform);
	}
	
	void FixedUpdate () {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Grounded") == false) return;
        mouseLook.LookRotation(transform,Camera.main.transform);
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (Mathf.Abs(h) > 0 || Mathf.Abs(v) > 0)
        {
            transform.Translate(new Vector3(h, 0, v) * speed*Time.deltaTime,Space.Self);
            float res = Mathf.Max(Mathf.Abs(h), Mathf.Abs(v));
            forward = res;
            anim.SetFloat("Forward", res);
        }
	}
}
