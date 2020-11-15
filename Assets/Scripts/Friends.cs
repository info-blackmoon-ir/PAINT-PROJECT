using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class Friends {

    public int id { get; set; }
    public string username { get; set; }
    public string profile { get; set; }
    public Friends(int id, string username, string profile)
    {
        this.id = id;
        this.username = username;
        this.profile = profile;
    }

}

[Serializable]
public class FriendsData
{
    public Friends[] friends;
}
