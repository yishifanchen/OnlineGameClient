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
    private AudioManager audioMng;
    private PoolManager poolMng;

    private bool isEnterPlaying = false;
    private void Start()
    {
        InitManager();
    }
    private void Update()
    {
        UpdateManager();
        if (isEnterPlaying)
        {
            EnterPlaying();
            isEnterPlaying = false;
        }
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
        audioMng = new AudioManager(this);
        poolMng = new PoolManager(this);

        uiMng.OnInit();
        requestMng.OnInit();
        playerMng.OnInit();
        clientMng.OnInit();
        cameraMng.OnInit();
        audioMng.OnInit();
        poolMng.OnInit();
    }
    private void UpdateManager()
    {
        uiMng.Update();
        requestMng.Update();
        playerMng.Update();
        clientMng.Update();
        cameraMng.Update();
        audioMng.Update();
        poolMng.Update();
    }
    private void DestroyManager()
    {
        uiMng.OnDestroy();
        requestMng.OnDestroy();
        playerMng.OnDestroy();
        clientMng.OnDestroy();
        cameraMng.OnDestroy();
        audioMng.OnDestroy();
        poolMng.OnDestroy();
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
    public void SetUserData(UserData ud)
    {
        playerMng.UserData = ud;
    }
    public UserData GetUserData()
    {
        return playerMng.UserData;
    }
    public void SetCurrentRoleType(RoleType rt)
    {
        playerMng.SetCurrentRoleType(rt);
    }
    public GameObject GetCurrentRoleGameObject()
    {
        return playerMng.GetCurrentRoleGameObject();
    }
    public void EnterPlayingSync()
    {
        isEnterPlaying = true;
    }
    private void EnterPlaying()
    {
        playerMng.SpawnRoles();
        cameraMng.FollowRole();
    }
    public void StartPlaying()
    {
        playerMng.AddControlScript();
        playerMng.CreatSyncRequest();
    }
    public void PlayBgSound(string soundName)
    {
        audioMng.PlayBgSound(soundName);
    }
    public void PlayNormalSound(string soundName,float volume=1)
    {
        audioMng.PlayNormalSound(soundName, volume);
    }
    public void SendAttack(int damage)
    {
        playerMng.SendAttack(damage);
    }
    public void UpdateResult(int totalCount,int winCount)
    {
        playerMng.UodateResult(totalCount,winCount);
    }
}
