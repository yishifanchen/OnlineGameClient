using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BasePanel {
    private Text timerLabel;
    private int time = -1;
    private Button quitButton;
    private void Start()
    {
        timerLabel = transform.Find("TimerLabel").GetComponent<Text>();
        quitButton = transform.Find("QuitButton").GetComponent<Button>();
        quitButton.onClick.AddListener(OnQuitBtnClick);
        quitButton.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (time > -1)
        {
            ShowTime();
            time = -1;
        }
    }
    public override void OnEnter()
    {
        gameObject.SetActive(true);
    }
    public override void OnPause()
    {
        HideAnim();
    }
    public override void OnResume()
    {
        EnterAnim();
    }
    public override void OnExit()
    {
        HideAnim();
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
    private void OnQuitBtnClick()
    {

    }
    public void ShowTimeSync(int time)
    {
        this.time = time;
    }
    private void ShowTime()
    {
        timerLabel.gameObject.SetActive(true);
        Color tempColor = timerLabel.color;
        timerLabel.transform.localScale = Vector3.one;
        tempColor.a = 1;
        timerLabel.color = tempColor;
        timerLabel.text = time.ToString();
        timerLabel.transform.DOScale(2, 0.3f).SetDelay(0.3f);
        timerLabel.DOFade(0, 0.3f).SetDelay(0.3f).OnComplete(() => timerLabel.gameObject.SetActive(false));
    }
}
