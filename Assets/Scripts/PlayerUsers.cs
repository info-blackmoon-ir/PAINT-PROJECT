using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUsers :IComparable<PlayerUsers> {

    public string name;
    public int score;

    public PlayerUsers(string userid,int newscore)
    {
        name = userid;
        score = newscore;
    }

    public int CompareTo(PlayerUsers other)
    {
        if(other == null)
        {
            return 1;
        }
        return score;
    }
}
