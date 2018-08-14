using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class CreatRoomRequest : BaseRequest {
    private RoomPanel roomPanel;
    public override void Awake()
    {
        actionCode = ActionCode.CreateRoom;
        requestCode = RequestCode.Room;
        base.Awake(); 
    }
    public void SetPanel(BasePanel panel)
    {
        roomPanel = panel as RoomPanel;
    }
    public override void SendRequest()
    {
        base.SendRequest("r");
    }
    public override void OnResponse(string data)
    {
        string[] strs = data.Split(',');
        ReturnCode returnCode = (ReturnCode)int.Parse(strs[0]);
        RoleType roleType = (RoleType)int.Parse(strs[1]);
        if (returnCode == ReturnCode.Success)
        {
            roomPanel.SetLocalPlayerResSync();
        }
    }
}
