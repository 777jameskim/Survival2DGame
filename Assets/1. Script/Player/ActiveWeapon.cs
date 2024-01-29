    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] private Transform fireRot;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Transform firePos;
    [SerializeField] private Bullet bullet;

    private JSONdata.ActiveWeaponJSONdata awjd;
    [SerializeField] private ActiveWeapons awID;

    private Transform bulletParent;
    private float fireDelay;
    private float fireTimer;
    private float power;

    // Start is called before the first frame update
    void Start()
    {
        awjd = JSONdata.Instance.activeweaponJDB.activeweapon[(int)awID];
    }

    // Update is called once per frame
    void Update()
    {
        if (fireTimer <= fireDelay) fireTimer += Time.deltaTime;
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
            firePos.localPosition = new Vector3(awjd.fireposX, awjd.fireposY, 0);
            firePos.localRotation = Quaternion.Euler(new Vector3(0, 0, -90));
        }
        else
        {
            sr.flipX = true;
            firePos.localPosition = new Vector3(-awjd.fireposX, awjd.fireposY, 0);
            firePos.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }
    }

    public void SetAWdata(Transform parent, float speed, float power)
    {
        this.bulletParent = parent;
        this.fireDelay = 1 / speed;
        this.power = power;
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
            }
            else
            {
                b.Power = power;
                b.transform.position = firePos.position;
                b.transform.rotation = fireRot.rotation;
                b.gameObject.SetActive(true);
            }
            b.InitPos = firePos.position;
        }
    }
}
