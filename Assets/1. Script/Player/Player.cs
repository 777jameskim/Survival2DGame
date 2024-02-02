using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(SpriteAnimation))]

public class Player : MonoBehaviour
{
    private SpriteAnimation sa;
    private SpriteRenderer sr;
    private PlayerState state = PlayerState.Stand;
    private PlayerData data = new PlayerData();

    private GameManager.PlayerData pData;

    private List<Sprite> stand;
    private List<Sprite> run;
    private List<Sprite> dead;
    [SerializeField] private UnityEngine.UI.Image hpImage;

    public float HP
    {
        get { return data.HP; }
        set
        {
            data.HP = value;
            UI.Instance.RefreshHP(hpImage);

            if(value <= 0 && state != PlayerState.Dead)
            {
                state = PlayerState.Dead;
                sa.SetSprite(dead, 0.5f, Dead, 0.5f);
            }
        }
    }
    public float MaxHP
    {
        get { return data.maxHP; }
        set
        {
            data.maxHP = value;
            UI.Instance.RefreshHP(hpImage);
        }
    }

    public int EXP
    {
        get { return data.EXP; }
        set
        {
            data.EXP = value;
            UI.Instance.RefreshEXP();
        }
    }

    public int MaxEXP
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
    [SerializeField] private ActiveWeapon aw;
    private ActiveWeapons awID;
    public ActiveWeapons AWtype
    {
        get { return awID; }
        set
        {
            awID = value;
            aw.SetAW(awID);
        }
    }
    private float ATKmod
    {
        get { return pData.atkMod; }
        set
        {
            pData.atkMod = value;
            aw.SetMod(value, FIREmod);
        }
    }
    private float FIREmod
    {
        get { return pData.fireMod; }
        set
        {
            pData.fireMod = value;
            aw.SetMod(ATKmod, value);
        }
    }

    //Passive Weapon
    [SerializeField] private PassiveWeapon[] pws;
    [SerializeField] private Transform pwRot;
    [SerializeField] private Transform pwStock;
    private PassiveWeapons pwID;
    public PassiveWeapons PWtype
    {
        get { return pwID; }
        set
        {
            PWchange(pwID, value);
            pwID = value;
        }
    }


    private void Awake()
    {
        pData = GameManager.Instance.pDatas[GameParams.charSelect];
        stand = pData.stand.ToList();
        run = pData.run.ToList();
        dead = pData.dead.ToList();
    }

    // Start is called before the first frame update
    void Start()
    {
        sa = GetComponent<SpriteAnimation>();
        sr = GetComponent<SpriteRenderer>();
        sa.SetSprite(stand, GameParams.playerStandDelay);
        data.findRange = 5;
        data.HP = 20;
        data.maxHP = 20;
        data.speed = 3;
        data.EXP = 0;
        data.maxEXP = 100;
        data.level = 1;
        data.killCount = 0;

        aw.SetAW(awID);
        aw.SetMod(pData.atkMod, pData.fireMod);

        HP = HP;
        EXP = EXP;
        Level = Level;
        KillCount = KillCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameParams.state != GameState.Play
            || state == PlayerState.Dead
            || UI.Instance == null) return;

        //float x = Input.GetAxis("Horizontal") * Time.deltaTime * data.speed * pData.speedMod;
        //float y = Input.GetAxis("Vertical") * Time.deltaTime * data.speed * pData.speedMod;

        float x = UI.Instance.joystick.Horizontal * Time.deltaTime * data.speed * pData.speedMod;
        float y = UI.Instance.joystick.Vertical * Time.deltaTime * data.speed * pData.speedMod;
        float clampX = Mathf.Clamp(transform.position.x + x, -GameParams.playerX, GameParams.playerX);
        float clampY = Mathf.Clamp(transform.position.y + y, -GameParams.playerY, GameParams.playerY);

        transform.position = new Vector2(clampX, clampY);

        if (x > 0 && sr.flipX)
        {
            sr.flipX = false;
            aw.SetHand(true);
            aw.SetPoint(true);
        }
        else if (x < 0 && !sr.flipX)
        {
            sr.flipX = true;
            aw.SetHand(false);
            aw.SetPoint(false);
        }

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
        PWrotate();

        if(Input.GetKeyDown(KeyCode.F1))
            PWgenerate();
        if (Input.GetKeyDown(KeyCode.F2))
            PWdelete();
    }

    private void FindMonster()
    {
        GameObject[] monObjs = GameObject.FindGameObjectsWithTag("monster");
        if (monObjs.Length == 0) return;
        float targetDis = data.findRange * pData.rangeMod;
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

        if (targetMon != null)
        {
            if (transform.position.x < targetMon.transform.position.x)
            { //enemy @ right
                aw.SetPoint(true);
                AWrotation(targetMon, false);
            }
            else if (transform.position.x > targetMon.transform.position.x)
            { // enemy @ left
                aw.SetPoint(false);
                AWrotation(targetMon, true);
            }
            aw.FireBullet();
        }
    }

    public void Dead()
    {
        UI.Instance.ShowDeadPanel();
        GameParams.state = GameState.Stop;
    }

    //Active Weapon
    public void AWrotation(Monster m, bool flip)
    {
        Vector2 vec = transform.position - m.transform.position;
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        aw.transform.rotation = Quaternion.AngleAxis((flip) ? angle - 90 : angle + 90, Vector3.forward);
    }

    //Passive Weapon
    private void PWgenerate()
    {
        int count = pwRot.childCount;
        PassiveWeapon pw = Pool.Instance.GetPW(pwID);
        if (pw == null) pw = Instantiate(pws[(int)pwID]);
        else pw.gameObject.SetActive(true);
        pw.transform.SetParent(pwRot);
        PWposition(count + 1);
    }
    private void PWdelete()
    {
        int count = pwRot.childCount;
        if (count == 0) return;
        PassiveWeapon pw = pwRot.GetChild(count - 1).GetComponent<PassiveWeapon>();
        pw.transform.SetParent(pwStock);
        Pool.Instance.SetPW(pw, pwID);
        PWposition(count - 1);
    }

    private void PWposition(int count)
    {
        if (count == 0) return;
        float angle = pwRot.GetChild(0).rotation.z * Mathf.Deg2Rad;
        float angleSpacing = 2 * Mathf.PI / count;
        foreach(Transform child in pwRot)
        {
            PassiveWeapon pw = child.GetComponent<PassiveWeapon>();
            child.localPosition = pw.AngleNorm(angle) * GameParams.passiveSpace;
            child.localRotation = Quaternion.Euler(Vector3.forward * -angle * Mathf.Rad2Deg);
            pw.angle = angle;
            angle += angleSpacing;
        }
    }

    private void PWchange(PassiveWeapons fromPW, PassiveWeapons toPW)
    {
        int count = pwRot.childCount;
        foreach (Transform child in pwRot)
            Pool.Instance.SetPW(child.GetComponent<PassiveWeapon>(), fromPW);
        for(int i = 0; i < count; i++)
        {
            pwRot.GetChild(0).SetParent(pwStock);
            PassiveWeapon pw = Pool.Instance.GetPW(toPW);
            if (pw == null) pw = Instantiate(pws[(int)toPW]);
            else pw.gameObject.SetActive(true);
            pw.transform.SetParent(pwRot);
        }
        PWposition(count);
    }

    private void PWrotate()
    {
        pwRot.Rotate(GameParams.passiveSpin * Mathf.Rad2Deg * Time.deltaTime * Vector3.back);
    }
}
