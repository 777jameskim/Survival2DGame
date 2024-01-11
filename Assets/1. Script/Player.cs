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
    }


}
