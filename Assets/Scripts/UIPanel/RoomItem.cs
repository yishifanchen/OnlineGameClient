using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour {
    public Text Username;
    public Text TotalCount;
    public Text WinCount;
    public Button joinButton;
    private int id;
    private RoomListPanel panel;
        
	void Start () {
        if (joinButton != null)
        {
            joinButton.onClick.AddListener(OnJoinBtnClick);
        }
	}
    public void SetRoomItemInfo(int id,string username, int totalCount,int winCount,RoomListPanel panel)
    {
        this.id = id;
        this.Username.text = username;
        this.TotalCount.text = totalCount.ToString();
        this.WinCount.text = winCount.ToString();
        this.panel = panel;
    }
	private void OnJoinBtnClick()
    {

    }
    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
