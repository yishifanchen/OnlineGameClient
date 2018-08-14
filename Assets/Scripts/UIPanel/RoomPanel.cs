using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanel : BasePanel
{
    private Text localPlayerUsername;
    private Text localPlayerTotalCount;
    private Text localPlayerWinCount;

    private Text enemyPlayerUsername;
    private Text enemyPlayerTotalCount;
    private Text enemyPlayerWinCount;

    private Transform panel1;
    private Transform panel2;

    private UserData ud1 = null;
    private UserData ud2 = null;

    private bool isPopPanel=false;

    private QuitRoomRequest quitRoomRequest;
    private void Start()
    {
        localPlayerUsername = transform.Find("ImageBG/PlayerList/PlayerInfoPanel1/NameLabel").GetComponent<Text>();
        localPlayerTotalCount = transform.Find("ImageBG/PlayerList/PlayerInfoPanel1/TotalCountLabel").GetComponent<Text>();
        localPlayerWinCount = transform.Find("ImageBG/PlayerList/PlayerInfoPanel1/WinCountLabel").GetComponent<Text>();

        enemyPlayerUsername = transform.Find("ImageBG/PlayerList/PlayerInfoPanel2/NameLabel").GetComponent<Text>();
        enemyPlayerTotalCount = transform.Find("ImageBG/PlayerList/PlayerInfoPanel2/TotalCountLabel").GetComponent<Text>();
        enemyPlayerWinCount = transform.Find("ImageBG/PlayerList/PlayerInfoPanel2/WinCountLabel").GetComponent<Text>();

        transform.Find("ImageBG/Buttons/ButtonQuit").GetComponent<Button>().onClick.AddListener(OnCloseBtnClick);

        quitRoomRequest = GetComponent<QuitRoomRequest>();
    }
    private void Update()
    {
        if (ud1!=null)
        {
            SetLocalPlayerRes(ud1.Username,ud1.TotalCount.ToString(),ud1.WinCount.ToString());
            ClearEnemyPlayerRes();
            if (ud2 != null)
            {
                SetEnemyPlayerRes(ud2.Username, ud2.TotalCount.ToString(), ud2.WinCount.ToString());
                ud1 = null;
                ud2 = null;
            }
        }
        if (isPopPanel)
        {
            uiMng.PopPanel();
            isPopPanel = false;
        }
    }
    public override void OnEnter()
    {
        base.OnEnter();
        EnterAnim();
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
        quitRoomRequest.SendRequest();
        //uiMng.PopPanel();
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
    public void SetLocalPlayerRes(string username,string totalCount,string winCount)
    {
        localPlayerUsername.text = "玩家:"+username;
        localPlayerTotalCount.text = "总场数:"+totalCount;
        localPlayerWinCount.text = "胜场:"+winCount;
    }
    public void SetEnemyPlayerRes(string username, string totalCount, string winCount)
    {
        enemyPlayerUsername.text = "玩家:" + username;
        enemyPlayerTotalCount.text = "总场数:" + totalCount;
        enemyPlayerWinCount.text = "胜场:" + winCount;
    }
    public void ClearEnemyPlayerRes()
    {
        enemyPlayerUsername.text = "等待玩家加入";
        enemyPlayerTotalCount.text = "等待玩家加入";
        enemyPlayerWinCount.text = "等待玩家加入";
    }
    public void SetLocalPlayerResSync()
    {
        ud1 = facade.GetUserData();
    }
    public void SetAllPlayerResSync(UserData ud1,UserData ud2)
    {
        this.ud1 = ud1;
        this.ud2 = ud2;
    }
    public void OnExitResponse()
    {
        isPopPanel = true;
    }
}
