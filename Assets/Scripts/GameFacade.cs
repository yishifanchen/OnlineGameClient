using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 外观类
/// </summary>
public class GameFacade : MonoBehaviour {
    public static GameFacade _instance;
    public static GameFacade Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance=GameObject.Find("GameFacade").GetComponent<GameFacade>();
            }
            return _instance;
        }
    }
    private UIManager uiMng;
    private RequestManager requestMng;
    private PlayerManager playerMng;
    private ClientManager clientMng;
    private CameraManager cameraMng;
    private void Start()
    {
        InitManager();
    }
    private void Update()
    {
        UpdateManager();
    }
    private void OnDestroy()
    {
        DestroyManager();
    }
    private void InitManager()
    {
        uiMng = new UIManager(this);
        requestMng = new RequestManager(this);
        playerMng = new PlayerManager(this);
        clientMng = new ClientManager(this);
        cameraMng = new CameraManager(this);

        uiMng.OnInit();
        requestMng.OnInit();
        playerMng.OnInit();
        clientMng.OnInit();
        cameraMng.OnInit();
    }
    private void UpdateManager()
    {
        uiMng.Update();
        requestMng.Update();
        playerMng.Update();
        clientMng.Update();
        cameraMng.Update();
    }
    private void DestroyManager()
    {
        uiMng.OnDestroy();
        requestMng.OnDestroy();
        playerMng.OnDestroy();
        clientMng.OnDestroy();
        cameraMng.OnDestroy();
    }
    public void AddRequest(ActionCode actionCode,BaseRequest request)
    {
        requestMng.AddRequest(actionCode,request);
    }
    public void RemoveRequest(ActionCode actionCode)
    {
        requestMng.RemoveRequest(actionCode);
    }
    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        clientMng.SendRequest(requestCode,actionCode,data);
    }
    public void HandleResponse(ActionCode actionCode,string data)
    {
        requestMng.HandleResponse(actionCode,data);
    }
}
