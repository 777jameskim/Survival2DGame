using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

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
    [SerializeField] private GameObject lvPanel;
    [SerializeField] private GameObject deadPanel;
    [SerializeField] private Transform deadTitle;
    [SerializeField] private Transform deadDesc;
    [SerializeField] private Transform deadButtons;

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

        lvPanel.SetActive(true);
    }

    public void OnClick(int index)
    {
        GameManager.Instance.P.Power += datas[index].AddPower;
        lvPanel.SetActive(false);
        GameParams.state = GameState.Play;
    }

    public void ShowDeadPanel()
    {
        deadPanel.SetActive(true);
        deadTitle.DOMoveY(1360, 1f)
            .SetEase(Ease.OutBounce)
            .SetDelay(0.5f)
            .OnComplete(() => 
                deadDesc.DOMoveY(1260, 1f)
                    .SetDelay(0.5f)
                    .OnComplete(() => deadButtons.gameObject.SetActive(true)));
    }

    public void OnExit()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void OnRestart()
    {
        SceneManager.LoadScene("GameScene");
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.P;
        lvPanel.SetActive(false);
        deadPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameParams.state != GameState.Play) return;
        timer += Time.deltaTime;
        timeText.text = $"{((int)timer / 60).ToString("00")}:{((int)timer % 60).ToString("00")}";
    }
}
