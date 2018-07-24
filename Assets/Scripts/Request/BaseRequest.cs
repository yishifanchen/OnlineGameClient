using System.Collections;
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
    public virtual void AddRequest(RequestCode requestCode)
    {

    }
    protected void SendRequest(string data)
    {
        Facade.SendRequest(requestCode, actionCode, data);
    }
}
