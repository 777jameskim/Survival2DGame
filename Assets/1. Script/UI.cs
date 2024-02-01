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

    [SerializeField] private ItemData[] items;
    [SerializeField] private GameObject lvPanel;
    [SerializeField] private GameObject deadPanel;
    [SerializeField] private Transform deadTitle;
    [SerializeField] private Transform deadDesc;
    [SerializeField] private Transform deadButtons;

    public FixedJoystick joystick;

    int awUp, pwUp, charUp;

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
        pwUp = Random.Range((int)ItemType.Shovel, (int)ItemType.Scythe);
        lvUIs[0].icon.sprite = items[pwUp].ItemIcon;
        lvUIs[0].titleText.text = items[pwUp].ItemName;
        lvUIs[0].desc1Text.text = items[pwUp].ItemDesc1;
        lvUIs[0].desc2Text.text = items[pwUp].ItemDesc2;
        awUp = Random.Range((int)ItemType.Rifle, (int)ItemType.Shotgun);
        lvUIs[1].icon.sprite = items[awUp].ItemIcon;
        lvUIs[1].titleText.text = items[awUp].ItemName;
        lvUIs[1].desc1Text.text = items[awUp].ItemDesc1;
        lvUIs[1].desc2Text.text = items[awUp].ItemDesc2;
        charUp = Random.Range((int)ItemType.Bag, (int)ItemType.Bullet);
        lvUIs[2].icon.sprite = items[charUp].ItemIcon;
        lvUIs[2].titleText.text = items[charUp].ItemName;
        lvUIs[2].desc1Text.text = items[charUp].ItemDesc1;
        lvUIs[2].desc2Text.text = items[charUp].ItemDesc2;

        lvPanel.SetActive(true);
    }

    public void OnClick(int index)
    {
        switch(index)
        {
            case 0: //pwUp
                break;
            case 1: //awUp
                ActiveWeapons type = (ActiveWeapons)awUp;
                break;
            case 2: //charUp
                break;
        }
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
        SceneManager.LoadScene("CharSelect");
    }

    public void OnRestart()
    {
        SceneManager.LoadScene("GameScene");
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    public void ToggleAW(int awID)
    {
        p.AWtype = (ActiveWeapons)awID;
    }

    private void Awake()
    {
        if (GameManager.Instance == null) SceneManager.LoadScene("TitleScene");
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

        if (Input.GetKeyDown(KeyCode.Alpha1))
            ToggleAW(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ToggleAW(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ToggleAW(2);
    }
}
