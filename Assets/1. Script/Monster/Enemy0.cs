using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy0 : Monster
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        moveDistance = 1;
        GameParams.monsterHitDelayTime = 1f;
        GameParams.monsterHP = 100;
        sa.SetSprite(sr, run, 0.2f);
    }
}
