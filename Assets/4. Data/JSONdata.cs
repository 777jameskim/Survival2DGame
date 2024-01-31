using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONdata : Singleton<JSONdata>
{
    [System.Serializable]
    public class MonsterJSONdata
    {
        public int HP;
        public int EXP;
        public float speed;
        public int power;
        public float hitdelaytime;
        public float atkrange;
        public float atkdelay;
        public float spritetransition;
    }

    [System.Serializable]
    public class ActiveWeaponJSONdata
    {
        public float speed;
        public float power;
    }

    [System.Serializable]
    public class MonsterJSON
    {
        public List<MonsterJSONdata> monster = new List<MonsterJSONdata>();
    }

    [System.Serializable]
    public class ActiveWeaponJSON
    {
        public List<ActiveWeaponJSONdata> activeweapon = new List<ActiveWeaponJSONdata>();
    }

    [SerializeField] private TextAsset monsterJSON;
    [SerializeField] private TextAsset activeweaponJSON;
    public MonsterJSON monsterJDB;
    public ActiveWeaponJSON activeweaponJDB;

    // Start is called before the first frame update
    void Start()
    {
        monsterJDB = JsonUtility.FromJson<MonsterJSON>(monsterJSON.text);
        activeweaponJDB = JsonUtility.FromJson<ActiveWeaponJSON>(activeweaponJSON.text);
    }
}
