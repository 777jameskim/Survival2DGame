using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : Singleton<UI>
{
    [System.Serializable]
    public class LvUI
    {
        public Image icon;
        public TMP_Text levelText;
        public TMP_Text titleText;
        public TMP_Text desc1Text;
        public TMP_Text desc2Text;
    }
    [SerializeField] private List<LvUI> lvUIs;

    [SerializeField] private Image expImage;
    [SerializeField] private TMP_Text expText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text killText;
    [SerializeField] private TMP_Text timeText;
    private Player p;
    private float timer;

    [SerializeField] private ItemData[] datas;
    [SerializeField] private GameObject lvPopUp;

    public void RefreshEXP()
    {
        expImage.rectTransform.sizeDelta = new Vector2(
            GameParams.expBarX * p.EXP / p.MaxEXP,
            GameParams.expBarY);
        expText.text = $"{p.EXP}/{p.MaxEXP}";

        if (p.EXP >= p.MaxEXP)
        {
            GameParams.state = GameState.Pause;
            p.Level++;
            p.EXP -= p.MaxEXP;
            p.MaxEXP = GameParams.stage * p.Level * 100;
            ShowLevelUpPanel();
        }
    }
    
    public void RefreshHP(Image hpImage)
    {
        if (p == null) p = GameManager.Instance.P;
        hpImage.rectTransform.sizeDelta = new Vector2(
            GameParams.hpBarX * p.HP / p.MaxHP,
            GameParams.hpBarY);
    }

    public void RefreshLevel()
    {
        levelText.text = $"Lv.{p.Level}";
    }

    public void RefreshKillCount()
    {
        killText.text = p.KillCount.ToString();
    }

    public void ShowLevelUpPanel()
    {
        lvUIs[0].icon.sprite = datas[0].ItemIcon;
        lvUIs[0].titleText.text = datas[0].ItemName;
        lvUIs[0].desc1Text.text = datas[0].ItemDesc1;
        lvUIs[0].desc2Text.text = datas[0].ItemDesc2;

        lvPopUp.SetActive(true);
    }

    public void OnClick(int index)
    {
        GameManager.Instance.P.Power += datas[index].AddPower;
        lvPopUp.SetActive(false);
        GameParams.state = GameState.Play;
    }

    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.P;
        lvPopUp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameParams.state != GameState.Play) return;
        timer += Time.deltaTime;
        timeText.text = $"{((int)timer / 60).ToString("00")}:{((int)timer % 60).ToString("00")}";
    }
}
