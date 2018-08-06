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

    private Transform panel1;
    private Transform panel2;

    private UserData ud1 = null;
    private void Start()
    {
        localPlayerUsername = transform.Find("ImageBG/PlayerList/PlayerInfoPanel1/NameLabel").GetComponent<Text>();
        localPlayerTotalCount = transform.Find("ImageBG/PlayerList/PlayerInfoPanel1/TotalCountLabel").GetComponent<Text>();
        localPlayerWinCount = transform.Find("ImageBG/PlayerList/PlayerInfoPanel1/WinCountLabel").GetComponent<Text>();
        transform.Find("ImageBG/Buttons/ButtonQuit").GetComponent<Button>().onClick.AddListener(OnCloseBtnClick);
    }
    private void Update()
    {
        if (ud1!=null)
        {
            SetLocalPlayerRes(ud1.Username,ud1.TotalCount.ToString(),ud1.WinCount.ToString());
            ud1 = null;
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
        uiMng.PopPanel();
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
        localPlayerUsername.text = username;
        localPlayerTotalCount.text = totalCount;
        localPlayerWinCount.text = winCount;
    }
}
