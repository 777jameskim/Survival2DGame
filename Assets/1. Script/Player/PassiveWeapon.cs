using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PassiveWeapon : MonoBehaviour
{
    public int Power { get; set; } = 1;
    [SerializeField] private float spinSpeed;
    [SerializeField] private float ySpeed;
    private float yOffset;
    private bool yTravel;
    public float angle;

    public Vector3 AngleNorm(float angle)
    {
        return new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameParams.state != GameState.Play) return;
        transform.Rotate(spinSpeed * Mathf.Rad2Deg * Time.deltaTime * Vector3.back);
        if (ySpeed != 0)
        {
            if (yTravel)
            {
                yOffset += ySpeed * Time.deltaTime;
                if (yOffset >= GameParams.passiveYtravel) yTravel = false;
            }
            else
            {
                yOffset -= ySpeed * Time.deltaTime;
                if (yOffset <= -GameParams.passiveYtravel) yTravel = true;
            }
            transform.localPosition = AngleNorm(angle) * (GameParams.passiveSpace + yOffset - 0.3f);
        }
    }
}
