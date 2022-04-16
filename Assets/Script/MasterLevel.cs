using System;
using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterLevel : MonoBehaviour
{
    [Header("paus")]
    public GameObject SetingsObject;
    private bool isPaus;
    [Header("Sidd")]
    public int Sidd;
    private int Life;
    private int MaxLife = 200000;
    private int time;
    private int TimeKill;
    private int TimeKillPlus = 60;
    private bool plei;
    public UiSpisac US;

    void Awake()
    {
        time = TimeInternet();
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            Life--;
            plei = true;
        }
        if (!PlayerPrefs.HasKey("Life"))
            PlayerPrefs.SetInt("Life", MaxLife);
        if (!PlayerPrefs.HasKey("Moneu"))
            PlayerPrefs.SetInt("Moneu", 0);
        Life = PlayerPrefs.GetInt("Life");
        TimeKill = PlayerPrefs.GetInt("TimeKill");
    }


    private void Start()
    {
        if (US == null)
            US = GetComponent<UiSpisac>();
        StartCoroutine(timePlus());
    }

    private void FixedUpdate()
    {
        if (Life < MaxLife && TimeKill + TimeKillPlus == time)
        {
            TimeKill = TimeKill + TimeKillPlus;
            Life++;
        }
    }

    public int Getlife()
    {
        return Life;
    }

    public void paus()
    {
        if (isPaus)
        {
            Time.timeScale = 1f;
            SetingsObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
            SetingsObject.SetActive(true);
        }
        isPaus = !isPaus;
    }

    public void Kill()
    {
        PlayerPrefs.SetInt("Life", Life);
        PlayerPrefs.SetInt("TimeKill", time);
        if (US != null)
            PlayerPrefs.SetInt("Moneu", US.GetMoneu());


        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator timePlus()
    {
        yield return new WaitForSeconds(1);
    }

    private WebClient client = new WebClient();
    public int TimeInternet()
    {
        string Sait = client.DownloadString("https://time100.ru/UTC");
        int secStroski = 0;
        int minStroski = 10004;
        int hasStroski = 10007;
        int deyStroski = 10138;
        int mauStroski = 10141;
        int gotStroski = 0;

        int sec = 0;
        int min = 0;
        int has = 0;
        int dey = 0;
        int mau = 0;
        int got = 0;

        string defolt = "";
        for (int i = minStroski; i < minStroski + 2; i++)
            defolt += Sait[i];
        min = Convert.ToInt32(defolt);

        defolt = "";
        for (int i = hasStroski; i < hasStroski + 2; i++)
            defolt += Sait[i];
        has = Convert.ToInt32(defolt);

        defolt = "";
        for (int i = deyStroski; i < deyStroski + 2; i++)
            defolt += Sait[i];
        dey = Convert.ToInt32(defolt);

        defolt = "";
        string[] mauI = {
            "€нварь", "февраль", "март", "апрел€", "май", "июнь",
            "июль", "август", "сент€брь", "окт€брь", "но€брь", "декабрь" };
        for (int i = mauStroski; i < mauStroski + 8; i++)
            defolt += Sait[i];
        string defolt2 = "";
        for (int i = 0; i < defolt.Length; i++)
            if (defolt[i] != ' ') defolt2 += defolt[i];
            else break;
        for (int i = 0; i < 12; i++)
            if (mauI[i] == defolt2)
            { mau = i + 1; break; }

        return min * 60 + has * 3600 + dey * 86400 + mau * 2592000;
    }
}
