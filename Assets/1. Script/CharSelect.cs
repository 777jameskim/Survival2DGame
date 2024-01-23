using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelect(int index)
    {
        GameParams.charSelect = index;
        SceneManager.LoadScene("GameScene");
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    public void OnClose()
    {
        Application.Quit();
    }
}
