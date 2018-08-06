using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class CreatRoomRequest : BaseRequest {
    public override void Awake()
    {
        actionCode = ActionCode.CreateRoom;
        requestCode = RequestCode.Room;
        base.Awake(); 
    }
    public override void SendRequest()
    {
        base.SendRequest("r");
    }
    public override void OnResponse(string data)
    {
        base.OnResponse(data);
    }
}
