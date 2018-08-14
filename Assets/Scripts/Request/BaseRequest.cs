﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class BaseRequest : MonoBehaviour {
    protected ActionCode actionCode=ActionCode.None;
    protected RequestCode requestCode=RequestCode.None;
    protected GameFacade facade;
    protected GameFacade Facade
    {
        get
        {
            if (facade == null)
            {
                facade = GameFacade.Instance;
            }
            return facade;
        }
    }
    public virtual void Awake()
    {
        Facade.AddRequest(actionCode,this);
    }
    protected void SendRequest(string data)
    {
        Facade.SendRequest(requestCode, actionCode, data);
    }
    public virtual void SendRequest() { }
    public virtual void OnResponse(string data) { }
    public virtual void OnDestroy()
    {
        if (facade != null)
        {
            facade.RemoveRequest(actionCode);
        }
    }
}
