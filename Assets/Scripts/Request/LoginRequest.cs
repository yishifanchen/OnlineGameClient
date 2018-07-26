﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class LoginRequest : BaseRequest {
    private LoginPanel loginPanel;
    public override void Awake()
    {
        loginPanel = GetComponent<LoginPanel>();
        requestCode = RequestCode.User;
        actionCode = ActionCode.Login;
        base.Awake();
    }
    public void SendRequest(string username,string password)
    {
        string data = username + "," + password;
        base.SendRequest(data);
    }
    public override void OnResponse(string data)
    {
        string[] strs = data.Split(',');
        ReturnCode returnCode = (ReturnCode)int.Parse(strs[0]);
        loginPanel.OnLoginResponse(returnCode);
        if (returnCode == ReturnCode.Success)
        {
            
        }
    }
}
