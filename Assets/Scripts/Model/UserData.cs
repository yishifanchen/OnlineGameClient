using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData {
    public int Id { get; set; }
    public string Username { get; set; }
    public int TotalCount { get; set; }
    public int WinCount { get; set; }
	public UserData(string userdata)
    {
        string[] strs = userdata.Split(',');
        this.Id = int.Parse(strs[0]);
        this.Username = strs[1];
        this.TotalCount = int.Parse(strs[2]);
        this.WinCount = int.Parse(strs[3]);
    }
    public UserData(string username,int totalCount,int winCount)
    {
        this.Username = username;
        this.TotalCount = totalCount;
        this.WinCount = winCount;
    }
}
