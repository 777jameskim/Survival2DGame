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

    public override void Init()
    {
        data.HP = 10;
        data.EXP = GameParams.stage * 10;
        data.speed = 1f;
        data.hitDelayTime = 0.3f;
        data.power = 1f;
        data.attackDelay = 0.5f;

        base.Init();
        data.attackRange = 1;
        sa.SetSprite(sr, run, 0.2f);
    }
}
