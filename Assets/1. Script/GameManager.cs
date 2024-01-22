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

    public void Awake()
    {
        if (GameManager.Instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void OnStart()
    {
        SceneManager.LoadScene("GameScene");
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }
}
