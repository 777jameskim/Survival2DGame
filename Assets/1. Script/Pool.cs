using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : Singleton<Pool>
{
    private Dictionary<MonsterType,Queue<Monster>> DMonster = new Dictionary<MonsterType, Queue<Monster>>();
    private Queue<EXP> QEXP = new Queue<EXP>();
    private Queue<Bullet> QBullet = new Queue<Bullet>();

    private void Start()
    {
        foreach (MonsterType type in Enum.GetValues(typeof(MonsterType)))
            DMonster.Add(type, new Queue<Monster>());
    }

    public Monster GetMonster(MonsterType type)
    {
        if (DMonster[type].Count == 0) return null;
        Monster m = DMonster[type].Dequeue();
        m.gameObject.tag = "monster";
        m.GetComponent<Collider2D>().enabled = true;
        return m;
    }

    public void SetMonster(Monster monster, MonsterType type)
    {
        monster.gameObject.SetActive(false);
        DMonster[type].Enqueue(monster);
    }

    public Bullet GetBullet()
    {
        if (QBullet.Count == 0) return null;
        Bullet b = QBullet.Dequeue();
        return b;
    }

    public void SetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        QBullet.Enqueue(bullet);
    }

    public EXP GetEXP()
    {
        if (QEXP.Count == 0) return null;
        EXP e = QEXP.Dequeue();
        return e;
    }

    public void SetEXP(EXP exp)
    {
        exp.gameObject.SetActive(false);
        QEXP.Enqueue(exp);
    }
}
