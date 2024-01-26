using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] private Transform fireRot;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Transform firePos;
    [SerializeField] private Bullet bullet;
    private Transform bulletParent;
    private float fireDelay;
    private float fireTimer;
    private float power;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (fireTimer <= fireDelay) fireTimer += Time.deltaTime;
    }

    public void SetFlip(bool flipX)
    {
        if (flipX)
        {
            transform.localPosition = new Vector3(-GameParams.activeX, GameParams.activeY, 0);
            sr.flipX = true;
        }
        else
        {
            transform.localPosition = new Vector3(GameParams.activeX, GameParams.activeY, 0);
            sr.flipX = false;
        }
    }

    public void SetAWdata(Transform parent, float delay, float power)
    {
        this.bulletParent = parent;
        this.fireDelay = delay;
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
