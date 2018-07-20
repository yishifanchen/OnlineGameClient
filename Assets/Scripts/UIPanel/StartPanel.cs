using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartPanel : BasePanel {
    private Button btnLogin;
    private Animator animBtnLogin;
    public override void OnEnter()
    {
        base.OnEnter();
        btnLogin = transform.Find("ButtonLogin").GetComponent<Button>();
        btnLogin.onClick.AddListener(OnBtnLoginClick);
        animBtnLogin = btnLogin.GetComponent<Animator>();
    }
    private void OnBtnLoginClick()
    {
        uiMng.PushPanel(UIPanelType.Login);
    }
    public override void OnPause()
    {
        base.OnPause();
        animBtnLogin.enabled = false;
        btnLogin.transform.DOScale(0, 0.3f).OnComplete(() => btnLogin.gameObject.SetActive(false));
    }
    public override void OnResume()
    {
        base.OnResume();
        btnLogin.gameObject.SetActive(true);
        btnLogin.transform.DOScale(1, 0.3f).OnComplete(() => animBtnLogin.enabled = true);
    }
}
