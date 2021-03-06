﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel {
    private Button loginButton;
    private Animator loginButtonAnim;
    public override void OnEnter()
    {
        base.OnEnter();
        loginButton = transform.Find("LoginButton").GetComponent<Button>();
        loginButton.onClick.AddListener(OnloginButtonClick);
        loginButtonAnim = loginButton.GetComponent<Animator>();
    }
    private void OnloginButtonClick()
    {
        PlayClickSound();
        uiMng.PushPanel(UIPanelType.Login);
    }
    public override void OnPause()
    {
        base.OnPause();
        loginButtonAnim.enabled = false;
        loginButton.transform.DOScale(0, 0.3f).OnComplete(() => loginButton.gameObject.SetActive(false));
    }
    public override void OnResume()
    {
        base.OnResume();
        loginButton.gameObject.SetActive(true);
        loginButton.transform.DOScale(1, 0.3f).OnComplete(() => loginButtonAnim.enabled = true);
    }
}
