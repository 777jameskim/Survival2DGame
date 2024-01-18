using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(SpriteAnimation))]

public class Player : MonoBehaviour
{
    [SerializeField] private List<Sprite> stand;
    [SerializeField] private List<Sprite> run;
    [SerializeField] private List<Sprite> dead;

    private SpriteAnimation sa;
    private SpriteRenderer sr;
    private PlayerState state = PlayerState.Stand;
    private PlayerData data = new PlayerData();

    public float EXP
    {
        get { return data.EXP; }
        set
        {
            data.EXP = value;
            UI.Instance.RefreshEXP();
        }
    }

    public float MaxEXP
    {
        get { return data.maxEXP; }
        set
        {
            data.maxEXP = value;
            UI.Instance.RefreshEXP();
        }
    }
    public int Level
    {
        get { return data.level; }
        set
        {
            data.level = value;
            UI.Instance.RefreshLevel();
        }
    }
    public int KillCount
    {
        get { return data.killCount; }
        set
        {
            data.killCount = value;
            UI.Instance.RefreshKillCount();
        }
    }

    //Active Weapon
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform fireRot;
    [SerializeField] private Transform firePos;
    [SerializeField] private Transform bulletParent;
    [SerializeField] private float fireDelay;
    private float fireTimer;

    //Passive Weapon
    [SerializeField] private Transform passiveRot;

    // Start is called before the first frame update
    void Start()
    {
        data.EXP = 0;
        data.findRange = 5;
        data.HP = 50;
        data.killCount = 0;
        data.level = 1;
        data.maxEXP = 100;
        data.speed = 3;
        sa = GetComponent<SpriteAnimation>();
        sr = GetComponent<SpriteRenderer>();
        sa.SetSprite(stand, GameParams.playerStandDelay);
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * data.speed;
        float y = Input.GetAxis("Vertical") * Time.deltaTime * data.speed;
        float clampX = Mathf.Clamp(transform.position.x + x, -GameParams.playerX, GameParams.playerX);
        float clampY = Mathf.Clamp(transform.position.y + y, -GameParams.playerY, GameParams.playerY);

        transform.position = new Vector2(clampX, clampY);

        if (x > 0) sr.flipX = false;
        else if (x < 0) sr.flipX = true;

        if ((x != 0 || y != 0) && state == PlayerState.Stand)
        {
            state = PlayerState.Run;
            sa.SetSprite(run, GameParams.playerRunDelay);
        }
        else if (x == 0 && y == 0 && state == PlayerState.Run)
        {
            state = PlayerState.Stand;
            sa.SetSprite(stand, GameParams.playerStandDelay);
        }

        FindMonster();
        PassiveWeapon();
    }

    private void FindMonster()
    {
        GameObject[] monObjs = GameObject.FindGameObjectsWithTag("monster");
        if (monObjs.Length == 0) return;
        float targetDis = data.findRange;
        Monster targetMon = null;
        foreach (GameObject monObj in monObjs)
        {
            Monster mon = monObj.GetComponent<Monster>();
            float monDis = Vector2.Distance(transform.position, monObj.transform.position);
            if (monDis < targetDis)
            {
                targetDis = monDis;
                targetMon = mon;
            }
        }

        if (fireTimer <= fireDelay) fireTimer += Time.deltaTime;
        if (targetMon != null)
        {
            FirePosRotation(targetMon);
            FireBullet();
        }
    }

    public void FirePosRotation(Monster m)
    {
        Vector2 vec = transform.position - m.transform.position;
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        fireRot.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }

    private void FireBullet()
    {
        if (fireTimer >= fireDelay)
        {
            fireTimer = 0;
            Bullet b = Instantiate(bullet, firePos);
            b.transform.SetParent(bulletParent);
        }
    }

    private void PassiveWeapon()
    {
        passiveRot.Rotate(Vector3.back * Time.deltaTime * GameParams.passiveSpin * Mathf.Rad2Deg);
    }
}
