using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonL : Monster
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        monsterID = MonsterType.SkeletonL;
        base.Init();
    }
}