using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Monster
{
    [SerializeField] private int mjdIndex;
    private JSONdata.MonsterJSONdata mjd;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        data.HP = 10;
        data.EXP = GameParams.stage * 10;
        mjd = JSONdata.Instance.monsterJDB.monster[mjdIndex];
        data.speed = mjd.speed;
        data.power = mjd.power;
        data.hitDelayTime = mjd.hitdelaytime;
        data.attackRange = mjd.atkrange;
        data.attackDelay = mjd.atkdelay;

        base.Init();
        sa.SetSprite(sr, run, 0.2f);
    }
}
