using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Monster[] mons;
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject[] sps;
    [SerializeField] private float spawnTime;
    private float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnTime)
        {
            spawnTimer = 0;
            int spawnIndex = Random.Range(0, GameParams.stage - 1);
            Monster newmon = Instantiate(mons[spawnIndex], Return_RandomPosition(), Quaternion.identity);
            newmon.transform.SetParent(parent);
        }
    }

    Vector3 Return_RandomPosition()
    {
        GameObject rangeObject = sps[Random.Range(0, sps.Length)].gameObject;
        BoxCollider2D rangeCollider = rangeObject.GetComponent<BoxCollider2D>();

        Vector3 originPosition = rangeObject.transform.position;
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = rangeCollider.bounds.size.x;
        float range_Y = rangeCollider.bounds.size.y;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Y = Random.Range((range_Y / 2) * -1, range_Y / 2);
        Vector3 RandomPostion = new Vector3(range_X, range_Y, 0f);

        Vector3 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
    }
}
