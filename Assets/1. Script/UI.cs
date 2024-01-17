using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : Singleton<UI>
{
    [SerializeField] private Image expImage;
    [SerializeField] private TMP_Text expText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text killText;
    private Player p;

    public void RefreshEXP()
    {
        expImage.rectTransform.sizeDelta = new Vector2(
            GameParams.expBarX * p.EXP / p.MaxEXP, 
            GameParams.expBarY);
        expText.text = $"{p.EXP}/{p.MaxEXP}";

        if(p.EXP >= p.MaxEXP)
        {
            p.Level++;
            p.EXP -= p.MaxEXP;
            p.MaxEXP = GameParams.stage * p.Level * 100;
        }
    }

    public void RefreshLevel()
    {
        levelText.text = $"Lv.{p.Level}";
    }
    
    public void RefreshKillCount()
    {
        killText.text = p.KillCount.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.P;
        RefreshEXP();
        RefreshLevel();
        RefreshKillCount();
    }
}
