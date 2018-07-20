using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoginPanel : BasePanel {
    private Button loginButton;
    private Button registerButton;
    private Button closeButton;
    private InputField usernameIF;
    private InputField passwordIF;
    private void Start()
    {
        loginButton = transform.Find("ButtonLogin").GetComponent<Button>();
        registerButton= transform.Find("ButtonRegister").GetComponent<Button>();
        closeButton = transform.Find("ButtonClose").GetComponent<Button>();
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
        uiMng.PopPanel();
    }
    private void OnLoginBtnClick()
    {
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
    }
    private void OnRegisterBtnClick()
    {
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
}
