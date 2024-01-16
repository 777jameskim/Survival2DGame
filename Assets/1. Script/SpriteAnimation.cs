using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class SpriteAnimation : MonoBehaviour
{
    private SpriteRenderer sr;
    private List<Sprite> sprites;

    private int count;
    private float timer;
    private float delay;

    private float endTimer;
    private float endTime;
    private UnityAction action;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sprites.Count == 0) return;

        if (endTime != 0)
        {
            endTimer += Time.deltaTime;
            if (endTimer >= endTime)
            {
                endTime = 0;
                action?.Invoke();
            }
        }

        if (sprites.Count == 1)
        {
            sr.sprite = sprites[0];
            return;
        }

        timer += Time.deltaTime;
        if(timer >= delay)
        {
            timer = 0;
            sr.sprite = sprites[count++];
            if (sprites.Count <= count) count = 0;
        }
    }

    public void Initialize(List<Sprite> sprites, float delay)
    {
        this.sprites = sprites;
        this.delay = delay;

        count = 1;
        timer = 0;
        action = null;
        endTime = 0;
        endTimer = 0;

        if (sr == null) sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprites[0];
    }

    public void SetSprite(List<Sprite> sprites, float delay)
    {
        Initialize(sprites, delay);
    }

    public void SetSprite(List<Sprite> sprites, float delay, UnityAction action, float endTime)
    {
        Initialize(sprites, delay);
        this.action = action;
        this.endTime = endTime;
    }

    public void SetSprite(SpriteRenderer sr, List<Sprite> sprites, float delay)
    {
        this.sr = sr;
        Initialize(sprites, delay);
    }
}
