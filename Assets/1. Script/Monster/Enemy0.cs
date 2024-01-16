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
        data.HP = 10;
        data.EXP = GameParams.stage * 10;
        data.speed = 1f;
        data.hitDelayTime = 0.3f;

        base.Init();
        data.attackRange = 1;
        sa.SetSprite(sr, run, 0.2f);
    }
}
