using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : Singleton<Pool>
{
    private Queue<Monster> QMonster = new Queue<Monster>();
    private Queue<Bullet> QBullet = new Queue<Bullet>();

    public Monster GetMonster()
    {
        if (QMonster.Count == 0) return null;
        Monster m = QMonster.Dequeue();
        m.GetComponent<Collider2D>().enabled = true;
        return m;
    }

    public void SetMonster(Monster monster)
    {
        monster.gameObject.SetActive(false);
        QMonster.Enqueue(monster);
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
}
