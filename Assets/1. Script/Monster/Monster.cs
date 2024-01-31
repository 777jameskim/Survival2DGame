using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    ZombieS,
    ZombieL,
    SkeletonS,
    SkeletonL,
    Tombstone
}

[RequireComponent(typeof(SpriteAnimation))]

public abstract class Monster : MonoBehaviour
{
    [SerializeField] protected List<Sprite> run;
    [SerializeField] protected List<Sprite> hit;
    [SerializeField] protected List<Sprite> dead;

    private JSONdata.MonsterJSONdata mjd;
    protected MonsterType monsterID;

    private Player p;
    protected SpriteRenderer sr;
    protected SpriteAnimation sa;
    protected MonsterData data = new MonsterData();
    private MonsterState state;
    private float hitTimer;
    private float attackTimer;
    private float spriteTransition;

    protected EXP exp;
    protected Transform expParent;

    public virtual void Init()
    {
        sr = GetComponent<SpriteRenderer>();
        sa = GetComponent<SpriteAnimation>();
        state = MonsterState.Run;
        gameObject.tag = "monster";

        mjd = JSONdata.Instance.monsterJDB.monster[(int)monsterID];
        data.HP = mjd.HP;
        data.EXP = mjd.EXP;
        data.speed = mjd.speed;
        data.power = mjd.power;
        data.hitDelayTime = mjd.hitdelaytime;
        data.attackRange = mjd.atkrange;
        data.attackDelay = mjd.atkdelay;
        spriteTransition = mjd.spritetransition;
        sa.SetSprite(sr, run, spriteTransition);
    }

    public void SetEXP(EXP exp, Transform parent)
    {
        this.exp = exp;
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
                sa.SetSprite(run, spriteTransition);
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
            GenerateEXP(data.EXP);
            p.KillCount++;
        }
    }

    private void GenerateEXP(int value)
    {
        foreach(EXPtype type in Enum.GetValues(typeof(EXPtype)))
        {
            while (value >= GameParams.EXPvalue[type])
            {
                EXP exp = Pool.Instance.GetEXP();
                if (exp == null)
                {
                    exp = Instantiate(this.exp);
                    exp.SetPlayer(p);
                    exp.transform.SetParent(expParent);
                }
                Vector3 randomPos = transform.position + new Vector3(
                    UnityEngine.Random.Range(-GameParams.expSpawnRange, GameParams.expSpawnRange),
                    UnityEngine.Random.Range(-GameParams.expSpawnRange, GameParams.expSpawnRange),
                    0);
                exp.SetSprite(type);
                exp.valueEXP = GameParams.EXPvalue[type];
                exp.transform.position = randomPos;
                exp.gameObject.SetActive(true);
                value -= GameParams.EXPvalue[type];
            }
        }
    }

    public void Dead()
    {
        Pool.Instance.SetMonster(this, monsterID);
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
