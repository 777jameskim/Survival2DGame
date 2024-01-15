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
    private MonsterState state = MonsterState.Run;
    private float hitTimer;
    protected float moveDistance = 0f;

    protected virtual void Init()
    {
        sr = GetComponent<SpriteRenderer>();
        sa = GetComponent<SpriteAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.P == null)
            return;

        // p가 타겟
        if (p == null)
            p = GameManager.Instance.P;

        if (Input.GetKeyDown(KeyCode.F1))
        {
            Hit(1);
        }

        if (state == MonsterState.Hit)
        {
            hitTimer -= Time.deltaTime;
            //Debug.Log("hit : " + hitTimer);
            if (hitTimer <= 0)
            {
                state = MonsterState.Run;
                sa.SetSprite(run, 0.2f);
            }
            else
                return;
        }

        // 타겟에게 이동
        float mDis = Vector2.Distance(p.transform.position, transform.position);
        if (mDis > moveDistance)
        {
            // 이동
            Vector2 dis = p.transform.position - transform.position;
            Vector3 dir = dis.normalized * Time.deltaTime * GameParams.monsterSpeed;

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
        if (GameParams.monsterHP <= 0)
            return;

        GameParams.monsterHP -= damage;
        hitTimer = GameParams.monsterHitDelayTime;
        state = MonsterState.Hit;
        sa.SetSprite(hit, 0.1f);
        Debug.Log("Hit");
        if (GameParams.monsterHP <= 0)
        {

        }
    }
}
