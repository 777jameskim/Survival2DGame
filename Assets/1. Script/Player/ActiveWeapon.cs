using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActiveWeapons
{
    Pistol,
    Automatic,
    Shotgun
}

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Transform firePos;
    [SerializeField] private Bullet bullet;

    private JSONdata.ActiveWeaponJSONdata awjd;
    private ActiveWeapons awID;

    [SerializeField] private Transform bulletParent;
    private float fireDelay;
    private float fireTimer;
    private float power;

    private Dictionary<ActiveWeapons, float> DmodPower = new Dictionary<ActiveWeapons, float>();
    private Dictionary<ActiveWeapons, float> DmodSpeed = new Dictionary<ActiveWeapons, float>();
    private float powerMod;
    private float speedMod;

    // Start is called before the first frame update
    void Start()
    {
        SetAW(awID);
    }

    public void SetAW(ActiveWeapons type)
    {
        if(DmodPower.Count == 0)
        {
            foreach(ActiveWeapons awType in System.Enum.GetValues(typeof(ActiveWeapons)))
            {
                DmodPower.Add(awType, 1.0f);
                DmodSpeed.Add(awType, 1.0f);
            }
        }
        awID = type;
        awjd = JSONdata.Instance.activeweaponJDB.activeweapon[(int)awID];
        sr.sprite = sprites[(int)awID];
        power = awjd.power * powerMod * DmodPower[type];
        fireDelay = 1 / (awjd.speed * speedMod * DmodSpeed[type]);
    }

    public void SetMod(float powerMod, float speedMod)
    {
        this.powerMod = powerMod;
        this.speedMod = speedMod;
    }

    // Update is called once per frame
    void Update()
    {
        if (fireTimer <= fireDelay) fireTimer += Time.deltaTime;
        if (sr.sprite != sprites[(int)awID]) SetAW(awID);
    }

    public void SetHand(bool right)
    {
        if (right)
            transform.localPosition = new Vector3(GameParams.activeX, GameParams.activeY, 0);
        else
            transform.localPosition = new Vector3(-GameParams.activeX, GameParams.activeY, 0);
    }

    public void SetPoint(bool right)
    {
        if (right)
        {
            sr.flipX = false;
            firePos.localPosition = new Vector3(GameParams.fireposX, GameParams.fireposY, 0);
            firePos.localRotation = Quaternion.Euler(new Vector3(0, 0, -90));
        }
        else
        {
            sr.flipX = true;
            firePos.localPosition = new Vector3(-GameParams.fireposX, GameParams.fireposY, 0);
            firePos.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }
    }

    public void FireBullet()
    {
        if (fireTimer >= fireDelay)
        {
            fireTimer = 0;
            Bullet b = Pool.Instance.GetBullet();
            if (b == null)
            {
                b = Instantiate(bullet, firePos);
                b.transform.SetParent(bulletParent);
                b.Power = power;
                b.GetComponent<SpriteRenderer>().sprite = b.sprites[(int)awID];
            }
            else
            {
                b.Power = power;
                b.sr.sprite = b.sprites[(int)awID];
                b.transform.position = firePos.position;
                b.transform.rotation = firePos.rotation;
                b.gameObject.SetActive(true);
            }
            b.InitPos = firePos.position;
        }
    }
}
