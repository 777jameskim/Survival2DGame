using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteAnimation))]

public abstract class Monster : MonoBehaviour
{
    [SerializeField] protected List<Sprite> run;
    [SerializeField] protected List<Sprite> hit;
    [SerializeField] protected List<Sprite> dead;

    private Player p;
    protected SpriteRenderer sr;
    protected SpriteAnimation sa;
    protected MonsterData data = new MonsterData();
    private MonsterState state = MonsterState.Run;
    private float hitTimer;

    protected virtual void Init()
    {
        sr = GetComponent<SpriteRenderer>();
        sa = GetComponent<SpriteAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.P == null || state == MonsterState.Dead) return;
        if (p == null) p = GameManager.Instance.P;

        if (state == MonsterState.Hit)
        {
            hitTimer -= Time.deltaTime;
            if (hitTimer <= 0)
            {
                state = MonsterState.Run;
                sa.SetSprite(run, 0.2f);
            }
            else
                return;
        }

        float mDis = Vector2.Distance(p.transform.position, transform.position);
        if (mDis > data.attackRange)
        {
            // 이동
            Vector2 dis = p.transform.position - transform.position;
            Vector3 dir = dis.normalized * Time.deltaTime * data.speed;

            // 방향
            if (dir.normalized.x > 0)
                sr.flipX = false;
            else if (dir.normalized.x < 0)
                sr.flipX = true;

            transform.Translate(dir);
        }
        else
        {
            // 공격
        }
    }

    public void Hit(int damage)
    {
        if (data.HP <= 0)
            return;

        data.HP -= damage;
        hitTimer = data.hitDelayTime;
        state = MonsterState.Hit;
        sa.SetSprite(hit, 0.1f);
        if (data.HP <= 0)
        {
            state = MonsterState.Dead;
            gameObject.tag = "Untagged";
            Destroy(GetComponent<Collider2D>());
            sa.SetSprite(dead, 0.2f, Dead, 2f);
        }
    }

    public void Dead()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet b = collision.GetComponent<Bullet>();
        if (b != null)
        {
            Hit(b.Power);
            Destroy(collision.gameObject);
        }
    }
}
