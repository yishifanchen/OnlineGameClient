using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListRoomRequest : BaseRequest {
    public override void Awake()
    {
        actionCode = ActionCode.ListRoom;
        requestCode = RequestCode.Room;
        base.Awake();
    }
    public override void SendRequest()
    {
        base.SendRequest("r");
    }
    public override void OnResponse(string data)
    {
        print(data);
    }
}
