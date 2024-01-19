using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : Singleton<Pool>
{
    private Queue<Monster> QMonster = new Queue<Monster>();
    private Queue<Bullet> QBullet = new Queue<Bullet>();

    public Monster GetMonster(Vector3 position)
    {
        if (QMonster.Count == 0) return null;
        Monster m = QMonster.Dequeue();
        m.transform.position = position;
        m.GetComponent<Collider2D>().enabled = true;
        m.gameObject.SetActive(true);
        return m;
    }

    public void SetMonster(Monster monster)
    {
        monster.gameObject.SetActive(false);
        QMonster.Enqueue(monster);
    }
}
