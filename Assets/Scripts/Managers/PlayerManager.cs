using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class PlayerManager : BaseManager {
    public PlayerManager(GameFacade facade) : base(facade) { }

    private UserData userData;
    public UserData UserData
    {
        get { return userData; }
        set { userData = value; }
    }
    private Dictionary<RoleType, RoleData> roleDataDict = new Dictionary<RoleType, RoleData>();
    private Transform rolePositions;
    private RoleType currentRoleType;
    private GameObject currentRoleGameObject;
    private GameObject remoteRoleGameObject;
    private GameObject playerSyncRequest;

    private ShootRequest shootRequest;
    private AttackRequest attackRequest;

    public override void OnInit()
    {
        rolePositions = GameObject.Find("RolePositions").transform;
        InitRoleDataDict();
    }
    public void UodateResult(int totalCount,int winCount)
    {
        UserData.TotalCount = totalCount;
        UserData.WinCount = winCount;
    }
    private void InitRoleDataDict()
    {
        roleDataDict.Add(RoleType.Blue, new RoleData(RoleType.Blue, "Hunter_BLUE", "Arrow_BLUE", "Explosion_BLUE", rolePositions.Find("BLUE")));
        roleDataDict.Add(RoleType.Red, new RoleData(RoleType.Red, "Hunter_RED", "Arrow_RED", "Explosion_RED", rolePositions.Find("RED")));
    }
    public void SetCurrentRoleType(RoleType rt)
    {
        currentRoleType = rt;
    }
    public GameObject GetCurrentRoleGameObject()
    {
        return currentRoleGameObject;
    }
    private RoleData GetRoleData(RoleType rt)
    {
        RoleData rd = null;
        roleDataDict.TryGetValue(rt, out rd);
        return rd;
    }
    public void SpawnRoles()
    {
        foreach(RoleData rd in roleDataDict.Values)
        {
            GameObject go = GameObject.Instantiate(rd.RolePrefab, rd.SpawnPosition, Quaternion.identity);
            go.tag = "Player";
            if (rd.RoleType == currentRoleType)
            {
                currentRoleGameObject = go;
                currentRoleGameObject.GetComponent<PlayerInfo>().isLocal = true;
                currentRoleGameObject.GetComponent<PlayerInfo>().roleType = currentRoleType;
                Transform cameraPos = currentRoleGameObject.transform.Find("CameraPos");
                Camera.main.transform.position = cameraPos.position;
                Camera.main.transform.localEulerAngles = cameraPos.localEulerAngles;
                Camera.main.transform.SetParent(cameraPos);
            }
            else
            {
                remoteRoleGameObject = go;
                remoteRoleGameObject.GetComponent<PlayerInfo>().roleType = currentRoleType==RoleType.Blue?RoleType.Red:RoleType.Blue;
            }
        }
    }
    public void AddControlScript()
    {
        currentRoleGameObject.AddComponent<PlayerMove>();
        PlayerAttack playerAttack = currentRoleGameObject.AddComponent<PlayerAttack>();
        RoleType rt = currentRoleGameObject.GetComponent<PlayerInfo>().roleType;
        RoleData rd = GetRoleData(rt);
        playerAttack.arrowPrefab = rd.ArrowPrefab;
        playerAttack.SetPlayerMng(this);
    }
    public void CreatSyncRequest()
    {
        playerSyncRequest = new GameObject("PlayerSyncRequest");
        playerSyncRequest.AddComponent<MoveRequest>().SetLocalPlayer(currentRoleGameObject.transform, currentRoleGameObject.GetComponent<PlayerMove>())
            .SetRemotePlayer(remoteRoleGameObject.transform);
        shootRequest = playerSyncRequest.AddComponent<ShootRequest>();
        shootRequest.playerMng = this;
        attackRequest = playerSyncRequest.AddComponent<AttackRequest>();
    }
    public void Shoot(GameObject arrowPrefab,Vector3 pos,Quaternion rotation)
    {
        //facade.PlayNormalSound(AudioManager.Sound_Sound_Alert);
        GameObject.Instantiate(arrowPrefab, pos, rotation).GetComponent<Arrow>().isLocal = true;
        shootRequest.SendRequest(arrowPrefab.GetComponent<Arrow>().roleType,pos,rotation.eulerAngles);
    }
    public void RometoShoot(RoleType rt,Vector3 pos,Vector3 rotation)
    {
        GameObject arrowPrefab = GetRoleData(rt).ArrowPrefab;
        Transform transform = GameObject.Instantiate(arrowPrefab).transform;
        transform.position = pos;
        transform.eulerAngles = rotation;
    }
    public void SendAttack(int damage)
    {
        attackRequest.SendRequest(damage);
    }
}
