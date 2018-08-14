using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class JoinRoomRequest : BaseRequest
{

    public override void Awake()
    {
        actionCode = ActionCode.JoinRoom;
        requestCode = RequestCode.Room;
        base.Awake();
    }
    public void SendRequest(int id)
    {
        base.SendRequest(id.ToString());
    }
    public override void OnResponse(string data)
    {
        
    }
}
