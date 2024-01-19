using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Power { get; set; }
    public float Speed { get; set; } = 10;

    // Update is called once per frame
    void Update()
    {
        if (GameParams.state != GameState.Play) return;
        transform.Translate(Vector2.up * Time.deltaTime * Speed);
    }
}
