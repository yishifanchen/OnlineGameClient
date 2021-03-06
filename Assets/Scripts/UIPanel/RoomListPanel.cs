﻿using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListPanel : BasePanel
{
    private RectTransform battleRes;
    private RectTransform roomList;
    private Text username;
    private Text totalCount;
    private Text winCount;
    private List<UserData> udList=null;
    private GameObject roomItemPrefab;
    private GridLayoutGroup roomLayout;

    private CreatRoomRequest createRoomRequest;
    private ListRoomRequest listRoomRequest;
    private JoinRoomRequest joinRoomRequest;
    private UserData ud1=null;
    private UserData ud2=null;

    private void Start()
    {
        battleRes = transform.Find("BattleRes").GetComponent<RectTransform>();
        roomList = transform.Find("RoomList").GetComponent<RectTransform>();
        username = battleRes.Find("Username").GetComponent<Text>();
        totalCount = battleRes.Find("TotalCount").GetComponent<Text>();
        winCount = battleRes.Find("WinCount").GetComponent<Text>();
        roomLayout = transform.Find("RoomList/Scroll View/Viewport/RoomLayout").GetComponent<GridLayoutGroup>();
        transform.Find("RoomList/CloseButton").GetComponent<Button>().onClick.AddListener(OnCloseBtnClick);
        transform.Find("RoomList/CreatRoomButton").GetComponent<Button>().onClick.AddListener(OnCreatRoomBtnClick);
        transform.Find("RoomList/RefreshButton").GetComponent<Button>().onClick.AddListener(OnRefreshBtnClick);
        roomItemPrefab = Resources.Load("UIPanel/RoomItem") as GameObject;
        createRoomRequest = GetComponent<CreatRoomRequest>();
        listRoomRequest = GetComponent<ListRoomRequest>();
        joinRoomRequest = GetComponent<JoinRoomRequest>();
        SetBattleRes();
    }
    private void Update()
    {
        if (udList != null)
        {
            LoadRoomItem(udList);
            udList = null;
        }
        if (ud1 != null && ud2 != null)
        {
            BasePanel panel = uiMng.PushPanel(UIPanelType.Room);
            (panel as RoomPanel).SetAllPlayerResSync(ud1,ud2);
            ud1 = null;ud2 = null;
        }
    }
    public override void OnEnter()
    {
        if(username!=null)
            SetBattleRes();
        EnterAnim();
        if (listRoomRequest==null)
            listRoomRequest = GetComponent<ListRoomRequest>();
        listRoomRequest.SendRequest();
    }
    public override void OnPause()
    {
        base.OnPause();
        HideAnim();
    }
    public override void OnResume()
    {
        base.OnResume();
        EnterAnim();
    }
    public override void OnExit()
    {
        base.OnExit();
        HideAnim();
    }
    private void OnCloseBtnClick()
    {
        PlayClickSound();
        uiMng.PopPanel();
    }
    private void OnRefreshBtnClick()
    {
        PlayClickSound();
        listRoomRequest.SendRequest();
    }
    private void OnCreatRoomBtnClick()
    {
        PlayClickSound();
        BasePanel panel = uiMng.PushPanel(UIPanelType.Room);
        createRoomRequest.SetPanel(panel);
        createRoomRequest.SendRequest();
    }
    public void OnJoinBtnClick(int id)
    {
        PlayClickSound();
        joinRoomRequest.SendRequest(id);
    }
    private void EnterAnim()
    {
        gameObject.SetActive(true);
        transform.localPosition = new Vector3(-2000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.3f);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.3f);
    }
    private void HideAnim()
    {
        transform.DOScale(0, 0.3f);
        transform.DOLocalMove(new Vector3(-2000, 0, 0), 0.3f).OnComplete(() => gameObject.SetActive(false));
    }
    private void SetBattleRes()
    {
        UserData ud = facade.GetUserData();
        username.text = ud.Username;
        totalCount.text = "总场数："+ud.TotalCount.ToString();
        winCount.text = "胜利："+ud.WinCount.ToString();
    }
    private void LoadRoomItem(List<UserData> udList)
    {
        RoomItem[] roomItems = roomLayout.GetComponentsInChildren<RoomItem>();
        foreach(RoomItem ri in roomItems)
        {
            ri.DestroySelf();
        }
        for(int i = 0; i < udList.Count; i++)
        {
            GameObject roomItem = GameObject.Instantiate(roomItemPrefab);
            roomItem.transform.SetParent(roomLayout.transform,false);
            UserData ud = udList[i];
            roomItem.GetComponent<RoomItem>().SetRoomItemInfo(ud.Id,ud.Username,ud.TotalCount,ud.WinCount,this);
        }
        roomLayout.GetComponent<RectTransform>().sizeDelta =
            new Vector2(
                roomLayout.GetComponent<RectTransform>().sizeDelta.x,
                udList.Count * (roomLayout.cellSize.y + roomLayout.spacing.y) + roomLayout.padding.top
                );
    }
    public void LoadRoomItemSync(List<UserData> udList)
    {
        this.udList = udList;
    }
    public void OnJoinResponse(ReturnCode returnCode,UserData ud1,UserData ud2)
    {
        switch (returnCode)
        {
            case ReturnCode.NotFound:
                uiMng.ShowMessageSync("房间被销毁，无法加入");
                break;
            case ReturnCode.Fail:
                uiMng.ShowMessageSync("房间人数已满，无法加入");
                break;
            case ReturnCode.Success:
                this.ud1 = ud1;
                this.ud2 = ud2;
                break;
        }
    }
    public void OnUpdateResultResponse(int totalCount,int winCount)
    {
        facade.UpdateResult(totalCount, winCount);
        SetBattleRes();
    }
}
