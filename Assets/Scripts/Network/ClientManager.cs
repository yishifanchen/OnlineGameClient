﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using Common;

public class ClientManager : BaseManager {
    private const string IP = "127.0.0.1";
    private const int PORT = 6688;
    private Socket clientSocket;
    private Message msg = new Message();
    public ClientManager(GameFacade facade) : base(facade) { }
    public override void OnInit()
    {
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            clientSocket.Connect(IP, PORT);
            Start();
        }
        catch(Exception e)
        {
            Debug.LogWarning("无法连接服务器，请检查您的网络！！"+e);
        }
        base.OnInit();
    }
    private void Start()
    {
        clientSocket.BeginReceive(msg.Data,msg.StartIndex,msg.RemainSize,SocketFlags.None, ReceiveCallback,null);
    }
    private void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            if (clientSocket == null || clientSocket.Connected == false) return;
            int count = clientSocket.EndReceive(ar);
            msg.ReadMessage(count, OnProcessDataCallback);
            Start();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
    private void OnProcessDataCallback(ActionCode actionCode,string data)
    {

    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        try
        {
            clientSocket.Close();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
