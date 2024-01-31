using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Sprite[] sprites;
    public SpriteRenderer sr;
    public float Power { get; set; }
    public float Speed { get; set; } = 10;
    public Vector3 InitPos;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameParams.state != GameState.Play) return;
        transform.Translate(Vector2.up * Time.deltaTime * Speed);
        if (Vector3.Distance(transform.position, InitPos) >= GameParams.bulletRange)
            Pool.Instance.SetBullet(this);
    }
}
