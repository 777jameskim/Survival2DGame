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
    private MonsterState state;
    private float hitTimer;
    private float attackTimer;

    protected List<EXP> exps;
    protected Transform expParent;

    public virtual void Init()
    {
        sr = GetComponent<SpriteRenderer>();
        sa = GetComponent<SpriteAnimation>();
        state = MonsterState.Run;
        gameObject.tag = "monster";
    }

    public void SetEXPs(List<EXP> exps, Transform parent)
    {
        this.exps = exps;
        this.expParent = parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameParams.state != GameState.Play) return;
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
            Vector2 dis = p.transform.position - transform.position;
            Vector3 dir = dis.normalized * Time.deltaTime * data.speed;

            if (dir.normalized.x > 0)
                sr.flipX = false;
            else if (dir.normalized.x < 0)
                sr.flipX = true;

            transform.Translate(dir);
        }
        else
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= data.attackDelay)
            {
                attackTimer = 0;
                p.HP -= data.power;
            }
        }
    }

    public void Hit(float damage)
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
            GetComponent<Collider2D>().enabled = false;
            sa.SetSprite(dead, 0.2f, Dead, 2f);
            EXP exp = Instantiate(exps[Random.Range(0, exps.Count)], transform.position, Quaternion.identity);
            exp.SetPlayer(p);
            exp.valueEXP = data.EXP;
            exp.transform.SetParent(expParent);
            p.KillCount++;
        }
    }

    public void Dead()
    {
        Pool.Instance.SetMonster(this);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet b = collision.GetComponent<Bullet>();
        if (b != null)
        {
            Hit(b.Power);
            Pool.Instance.SetBullet(b);
        }
        PassiveWeapon pw = collision.GetComponent<PassiveWeapon>();
        if (pw != null)
        {
            Hit(pw.Power);
        }
    }
}
