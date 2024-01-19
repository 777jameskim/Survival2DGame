using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EXPtype
{
    BRONZE,
    SILVER,
    GOLD
}

public class EXP : MonoBehaviour
{
    private Player p;
    [SerializeField] private Sprite[] sprite;
    public float valueEXP { get; set; } = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameParams.state != GameState.Play) return;
        if (p != null)
        {
            float distance = Vector2.Distance(transform.position, p.transform.position);
            if(distance <= GameParams.pickupDistance)
            {
                transform.position = Vector2.Lerp(transform.position, p.transform.position, Time.deltaTime * GameParams.itemSpeed);
                if(distance < GameParams.collectDistance)
                {
                    p.EXP += valueEXP;
                    Destroy(gameObject);
                }
            }
        }
    }

    public void SetPlayer(Player p)
    {
        this.p = p;
    }

    public void SetSprite(EXPtype type)
    {
        GetComponent<SpriteRenderer>().sprite = sprite[(int)type];
    }
}
