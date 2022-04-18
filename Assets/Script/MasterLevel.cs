using System;
using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MasterLevel : MonoBehaviour
{
    [Header("paus")]
    public GameObject SetingsObject;
    public GameObject PausObject;
    public GameObject EndObject;
    public GameObject PunktObject;
    public Animator menu;
    public static bool isPaus = false;//MasterLevel.isPaus
    [Header("AudioMixer")]
    public Animator mus;
    public AudioMixer am;
    public AudioMixerSnapshot[] AMSnap =new AudioMixerSnapshot[2];
    public static bool isMusic = true;//MasterLevel.isPaus
    [Header("Sidd")]
    private int Life;
    private int MaxLife = 200000;
    private int time;
    private int TimeKill;
    private int TimeKillPlus = 60;
    private bool plei;
    public UiSpisac US;
    public bool besmert;
    public Transform pleir;


    void Awake()
    {
        //time = TimeInternet();
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

        //Debug.Log("пипес у теб€ деног: " + PlayerPrefs.GetInt("Moneu").ToString());
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

    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
            reset();
    }


    public int Getlife()
    {
        return Life;
    }


    private bool isIen;
    public void paus(bool Stop = false)
    {
        if (Stop)
            isPaus = false;
        else if (isIen)
            return;



        PausObject.SetActive(isPaus);
        if (isMusic)
            AMSnap[isPaus ? 0 : 1].TransitionTo(0.5f);

        if (isPaus)
            StartCoroutine(pausOff());
        else
            SetingsObject.SetActive(true);

        mus.SetBool("mus", isMusic);
        isPaus = !isPaus;
    }

    public IEnumerator pausOff()
    {
        isIen = true;
        menu.SetBool("stop", true);
        yield return new WaitForSeconds(0.15f);
        SetingsObject.SetActive(false);
        isIen = false;
    }

    public void Music()
    {
        if (!isMusic)
            AMSnap[isPaus ? 1 : 0].TransitionTo(0.5f);
        else 
            AMSnap[2].TransitionTo(0.5f);

        isMusic = !isMusic;
        mus.SetBool("mus", isMusic);
    }





    public void reset()
    {
        if (isPaus) paus();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void vozrat()
    {
        SceneManager.LoadScene(0);
    }


    public void Kill()
    {
        PlayerPrefs.SetInt("Life", Life);
        PlayerPrefs.SetInt("TimeKill", time);
        if (US != null)
            PlayerPrefs.SetInt("Moneu", PlayerPrefs.GetInt("Moneu") + US.GetMoneu());

        if (besmert)
            paus();
        else
            end();
    }


    public Text mon;
    public Text Rec;
    private void end()
    {
        paus(true);
        EndObject.SetActive(true);
        PausObject.SetActive(false);
        SetingsObject.SetActive(false);
        SetingsObject.SetActive(false);
        PunktObject.SetActive(false);
        Rec.text = ((int)(pleir.position.x)).ToString();
        mon.text = US.GetMoneu().ToString();
    }





    public IEnumerator timePlus()
    {
        yield return new WaitForSeconds(1);
    }


    //private WebClient client = new WebClient();
    public int TimeInternet()
    {
        return 0;
        //string Sait = client.DownloadString("https://time100.ru/UTC");
        //int secStroski = 0;
        //int minStroski = 10004;
        //int hasStroski = 10007;
        //int deyStroski = 10138;
        //int mauStroski = 10141;
        //int gotStroski = 0;

        //int sec = 0;
        //int min = 0;
        //int has = 0;
        //int dey = 0;
        //int mau = 0;
        //int got = 0;

        //string defolt = "";
        //for (int i = minStroski; i < minStroski + 2; i++)
        //    defolt += Sait[i];
        //min = Convert.ToInt32(defolt);

        //defolt = "";
        //for (int i = hasStroski; i < hasStroski + 2; i++)
        //    defolt += Sait[i];
        //has = Convert.ToInt32(defolt);

        //defolt = "";
        //for (int i = deyStroski; i < deyStroski + 2; i++)
        //    defolt += Sait[i];
        //dey = Convert.ToInt32(defolt);

        //defolt = "";
        //string[] mauI = {
        //    "€нварь", "февраль", "март", "апрел€", "май", "июнь",
        //    "июль", "август", "сент€брь", "окт€брь", "но€брь", "декабрь" };
        //for (int i = mauStroski; i < mauStroski + 8; i++)
        //    defolt += Sait[i];
        //string defolt2 = "";
        //for (int i = 0; i < defolt.Length; i++)
        //    if (defolt[i] != ' ') defolt2 += defolt[i];
        //    else break;
        //for (int i = 0; i < 12; i++)
        //    if (mauI[i] == defolt2)
        //    { mau = i + 1; break; }

        //return min * 60 + has * 3600 + dey * 86400 + mau * 2592000;
    }
}
