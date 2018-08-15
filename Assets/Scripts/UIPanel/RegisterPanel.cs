using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : BasePanel {
    private InputField usernameIF;
    private InputField passwordIF;
    private InputField rePasswordIF;
    private RegisterRequest registerRequest;
    private void Start()
    {
        registerRequest = GetComponent<RegisterRequest>();
        usernameIF = transform.Find("UsernameLabel/InputField").GetComponent<InputField>();
        passwordIF = transform.Find("PasswordLabel/InputField").GetComponent<InputField>();
        rePasswordIF = transform.Find("RePasswordLabel/InputField").GetComponent<InputField>();
        transform.Find("CloseButton").GetComponent<Button>().onClick.AddListener(OnCloseBtnClick);
        transform.Find("RegisterButton").GetComponent<Button>().onClick.AddListener(OnRegisterBtnClick);
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
    private void OnRegisterBtnClick()
    {
        string msg="";
        if (string.IsNullOrEmpty(usernameIF.text))
        {
            msg += "用户名不能为空";
        }
        if (string.IsNullOrEmpty(passwordIF.text))
        {
            msg += "\n密码不能为空";
        }
        if (passwordIF.text!=rePasswordIF.text)
        {
            msg += "\n密码不一致";
        }
        if (msg != "")
        {
            uiMng.ShowMessage(msg);
            return;
        }
        registerRequest.SendRequest(usernameIF.text,passwordIF.text);
    }
    public void OnRegisterResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            uiMng.ShowMessageSync("注册成功！");
        }
        else
        {
            uiMng.ShowMessageSync("注册失败");
        }
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
