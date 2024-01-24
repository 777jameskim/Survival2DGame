using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONdata : Singleton<JSONdata>
{
    [System.Serializable]
    public class MonsterJSONdata
    {
        public float speed;
        public int power;
        public float hitdelaytime;
        public float atkrange;
        public float atkdelay;
    }

    [System.Serializable]
    public class MonsterJSON
    {
        public List<MonsterJSONdata> monster = new List<MonsterJSONdata>();
    }

    [SerializeField] private TextAsset monsterJSON;
    public MonsterJSON monsterJDB;

    // Start is called before the first frame update
    void Start()
    {
        monsterJDB = JsonUtility.FromJson<MonsterJSON>(monsterJSON.text);
    }
}
