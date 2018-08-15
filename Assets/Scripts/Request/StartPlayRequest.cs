using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class StartPlayRequest : BaseRequest {
    private bool isStartPlaying = false;
    private GamePanel gamePanel;
    public override void Awake()
    {
        actionCode = ActionCode.StartPlay;
        gamePanel = GetComponent<GamePanel>();
        base.Awake();
    }
    private void Update()
    {
        if (isStartPlaying)
        {
            facade.StartPlaying();
            isStartPlaying = false;
        }
    }
    public override void OnResponse(string data)
    {
        isStartPlaying = true;
        gamePanel.UIMng.PushPanelSync(UIPanelType.Null);
    }
}
