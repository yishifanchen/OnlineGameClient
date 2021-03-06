﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Common;

public class LoginPanel : BasePanel {
    private Button loginButton;
    private Button registerButton;
    private Button closeButton;
    private InputField usernameIF;
    private InputField passwordIF;
    private LoginRequest loginRequest;
    private void Start()
    {
        loginRequest = GetComponent<LoginRequest>();
        loginButton = transform.Find("LoginButton").GetComponent<Button>();
        registerButton= transform.Find("RegisterButton").GetComponent<Button>();
        closeButton = transform.Find("CloseButton").GetComponent<Button>();
        usernameIF= transform.Find("UsernameLabel/InputField").GetComponent<InputField>();
        passwordIF = transform.Find("PasswordLabel/InputField").GetComponent<InputField>();
        loginButton.onClick.AddListener(OnLoginBtnClick);
        registerButton.onClick.AddListener(OnRegisterBtnClick);
        closeButton.onClick.AddListener(OnCloseBtnClick);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            EventSystem.current.SetSelectedGameObject(usernameIF.isFocused ? passwordIF.gameObject : usernameIF.gameObject);
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
        PlayClickSound();
        uiMng.PopPanel();
    }
    private void OnLoginBtnClick()
    {
        PlayClickSound();
        string msg = "";
        if (string.IsNullOrEmpty(usernameIF.text))
        {
            msg += "用户名不能为空";
        }
        if (string.IsNullOrEmpty(passwordIF.text))
        {
            msg += "\n密码不能为空";
        }
        if (msg != "")
        {
            uiMng.ShowMessage(msg);
            return;
        }
        loginRequest.SendRequest(usernameIF.text, passwordIF.text);
    }
    private void OnRegisterBtnClick()
    {
        PlayClickSound();
        uiMng.PushPanel(UIPanelType.Register);
    }
    private void EnterAnim()
    {
        gameObject.SetActive(true);
        transform.localPosition = new Vector3(2000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.3f);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.3f);
    }
    private void HideAnim()
    {
        transform.DOScale(0, 0.3f);
        transform.DOLocalMove(new Vector3(2000, 0, 0), 0.3f).OnComplete(() => gameObject.SetActive(false));
    }
    public void OnLoginResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            uiMng.ShowMessageSync("登陆成功！");
            uiMng.PushPanelSync(UIPanelType.RoomList);
        }
        else
        {
            uiMng.ShowMessageSync("用户名或密码输入错误，无法登陆，请重新输入！");
        }
    }
}
