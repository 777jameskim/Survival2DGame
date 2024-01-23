using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private Player p;
    public Player P
    {
        get
        {
            if (p == null)
                p = FindObjectOfType<Player>();

            return p;
        }
    }

    [System.Serializable]
    public class PlayerData
    {
        public List<Sprite> stand;
        public List<Sprite> run;
        public List<Sprite> dead;

        public float speedMod;
        public float fireMod;
        public float atkMod;
        public float rangeMod;
    }

    public List<PlayerData> pDatas;

    public void Awake()
    {
        if (GameManager.Instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void OnStart()
    {
        SceneManager.LoadScene("CharSelect");
    }
}
