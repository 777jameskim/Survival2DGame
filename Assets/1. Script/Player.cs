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
    [SerializeField] private Transform firePos;
    private SpriteAnimation sa;
    private SpriteRenderer sr;
    private PlayerState state = PlayerState.Stand;

    // Start is called before the first frame update
    void Start()
    {
        sa = GetComponent<SpriteAnimation>();
        sr = GetComponent<SpriteRenderer>();
        sa.SetSprite(stand, GameParams.playerStandDelay);
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * GameParams.playerSpeed;
        float y = Input.GetAxis("Vertical") * Time.deltaTime * GameParams.playerSpeed;
        float clampX = Mathf.Clamp(transform.position.x + x, -GameParams.playerX, GameParams.playerX);
        float clampY = Mathf.Clamp(transform.position.y + y, -GameParams.playerY, GameParams.playerY);

        transform.position = new Vector2(clampX, clampY);

        if (x > 0) sr.flipX = false;
        else if (x < 0) sr.flipX = true;

        if((x != 0 || y != 0) && state == PlayerState.Stand)
        {
            state = PlayerState.Run;
            sa.SetSprite(run, GameParams.playerRunDelay);
        }
        else if(x == 0 && y == 0 && state == PlayerState.Run)
        {
            state = PlayerState.Stand;
            sa.SetSprite(stand, GameParams.playerStandDelay);
        }

        FindMonster();
    }

    private void FindMonster()
    {
        GameObject[] monObjs = GameObject.FindGameObjectsWithTag("monster");
        if (monObjs.Length == 0) return;
        float targetDis = GameParams.playerFindDistance;
        Monster targetMon = null;
        foreach (GameObject monObj in monObjs)
        {
            Monster mon = monObj.GetComponent<Monster>();
            float monDis = Vector2.Distance(transform.position, monObj.transform.position);
            if(monDis < targetDis)
            {
                targetDis = monDis;
                targetMon = mon;
            }
        }
        if (targetMon != null) FirePosRotation(targetMon);
    }

    public void FirePosRotation(Monster m)
    {
        Vector2 vec = transform.position - m.transform.position;
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        firePos.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }
}
