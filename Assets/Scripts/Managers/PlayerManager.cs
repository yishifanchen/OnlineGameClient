using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : BaseManager {
    public PlayerManager(GameFacade facade) : base(facade) { }

    private UserData userData;
    public UserData UserData
    {
        get { return userData; }
        set { userData = value; }
    }
}
