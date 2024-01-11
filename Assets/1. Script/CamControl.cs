using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    [SerializeField] private Player p;

    // Update is called once per frame
    void Update()
    {
        float clampX = Mathf.Clamp(p.transform.position.x, -GameParams.cameraX, GameParams.cameraX);
        float clampY = Mathf.Clamp(p.transform.position.y, -GameParams.cameraY, GameParams.cameraY);

        transform.position = new Vector3(clampX, clampY, GameParams.cameraZ);
    }
}
