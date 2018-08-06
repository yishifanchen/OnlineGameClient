using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanel : BasePanel
{
    private void Start()
    {

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
    private void OnCreatRoomBtnClick()
    {
        uiMng.PushPanel(UIPanelType.Room);
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
}
