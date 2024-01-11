using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpriteAnimation : MonoBehaviour
{
    private SpriteRenderer sr;
    private List<Sprite> sprites;

    private int count;
    private float timer;
    private float delay;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sprites.Count == 0) return;

        timer += Time.deltaTime;
        if(timer >= delay)
        {
            timer = 0;
            sr.sprite = sprites[count++];
            if (sprites.Count <= count) count = 0;
        }
    }

    public void SetSprite(List<Sprite> sprites, float delay)
    {
        count = 1;
        timer = 0;
        this.sprites = sprites;
        this.delay = delay;
        sr.sprite = sprites[0];
    }
}
