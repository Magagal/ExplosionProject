using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSlowTime : Singleton<ManagerSlowTime>
{
    public bool isSlowTime = true;

    public float normalTime = 1;
    public float slowTime = 0.1f;

    public void SetSlow(bool _slow)
    {
        isSlowTime = _slow;
    }
}
