using Common;
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
    private void Start()
    {
        battleRes = transform.Find("BattleRes").GetComponent<RectTransform>();
        roomList = transform.Find("RoomList").GetComponent<RectTransform>();
        username = battleRes.Find("Username").GetComponent<Text>();
        totalCount = battleRes.Find("TotalCount").GetComponent<Text>();
        winCount = battleRes.Find("WinCount").GetComponent<Text>();
        transform.Find("RoomList/ButtonClose").GetComponent<Button>().onClick.AddListener(OnCloseBtnClick);
        SetBattleRes();
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
    private void SetBattleRes()
    {
        UserData ud = facade.GetUserData();
        username.text = ud.Username;
        totalCount.text = "总场数："+ud.TotalCount.ToString();
        winCount.text = "胜利："+ud.WinCount.ToString();
    }
}
