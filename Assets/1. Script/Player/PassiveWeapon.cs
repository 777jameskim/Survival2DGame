using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PassiveWeapon : MonoBehaviour
{
    public int Power { get; set; } = 1;
    private float spinSpeed;

    // Start is called before the first frame update
    void Start()
    {
        spinSpeed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.back * Time.deltaTime * spinSpeed * Mathf.Rad2Deg);
    }
}
