using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class MasterLevel : MonoBehaviour
{
    public static MasterLevel singleton { get; private set; }


    [Header("paus")]
    public GameObject SetingsObject;
    public GameObject PausObject;
    public Animator menu;
    public static bool isPaus = false;//MasterLevel.isPaus
    public PostProcessVolume PPV;

    [Header("AudioMixer")]
    public Animator mus;
    public AudioMixer am;
    public AudioMixerSnapshot[] AMSnap = new AudioMixerSnapshot[2];
    public static bool isMusic = true;//MasterLevel.isMusic

    [Header("Life")]
    public TextMeshProUGUI distansia;
    public UiSpisac US;
    public bool besmert;
    public Transform pleir;

    [Header("END")]
    public GameObject EndObject;
    public GameObject PunktObject;
    public TextMeshProUGUI mon;
    public TextMeshProUGUI Rec;
    public MasterNet MN;
    public UnityEvent HitStart;

    [Header("PleirSposobnasti")]
    public PleirControlir PC;
    public bool onlain;
    private bool Zagruzka = true;
    private bool PleiBloc = false;
    public TextMeshProUGUI[] EroorVivod;

    public float TimeEroorStart;
    private float TimeEroor;

    public SpriteRenderer pleirRender;
    public Image pleirRender2;
    public Sprite[] PleirSprite;

    [Header("spesl")]
    public GameObject Obusenia;
    public GameObject record;
    public GameObject testFPS;
    public GameObject registor;
    [Header("Menu")]
    public TextMeshProUGUI PleirMenuMoney;
    public TextMeshProUGUI PleirMenuEnergia;
    public TextMeshProUGUI NFTMenuNeim;
    public TextMeshProUGUI NFTMenuEnergia;
    public TextMeshProUGUI timeObject;
    private jsonGETPleir resultat;
    public int NFTVID = 0;

    private bool isEnd = false;



    private void renderPleir(int znas)
    {
        pleirRender.sprite = PleirSprite[znas-1];
        pleirRender2.sprite= PleirSprite[znas-1];
    }

    public bool GetZagruzka()
    {
        return (Zagruzka || PleiBloc) || (resultat.NFT[NFTVID].Energia <= 0);
    }

    public void start()
    {
        singleton = this;
        recordTadlica();
        if (onlain&&resultat.nonitka)
            StartCoroutine(strtObuseniaCoroutine());
        isEnd = false;
    }

    private void Awake()
    {
        singleton = this;
        if (onlain)
            MN.GetRequest();
    }


    private void Start()
    {
        if (PC == null)
            PC = pleir.GetComponent<PleirControlir>();
        if (US == null)
            US = GetComponent<UiSpisac>();
        pausNOvisual();
        if (!onlain)
            renderPleir(1);
    }


    private void FixedUpdate()
    {
        if (onlain)
        {
            if (Zagruzka)
                conect();
            else if (PleiBloc)
            {
                if (TimeEroor <= 0) Zagruzka = true;
                TimeEroor -= Time.fixedDeltaTime;
            }
        }

        distansia.text = ((int)((pleir.position.x - PC.StrtPosision.x) / 5)).ToString();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            reset();
        if (Input.GetKeyDown(KeyCode.Q))
            testFPS.SetActive(!testFPS.activeInHierarchy);
    }


    private void conect()
    {
        string Otvet = MN.GetRequest();
        if (Otvet == "") return;
        Debug.Log("OtvetServer: " + Otvet);

        switch (Otvet)
        {
            case "EroorNet":
                erorit("EroorNet");
                return;
            case "registr":
                registor.SetActive(true);
                Zagruzka = false;
                PleiBloc = false;
                return;
            default:
                Destroy(registor);
                erorit("", false);
                Zagruzka = false;
                PleiBloc = false;
                break;
        }

        string Otvet2 = "";
        for (int i = 0; i < Otvet.Length; i++)
            if (Otvet[i] != '.')
                Otvet2 += Otvet[i];
            else
                Otvet2 += ',';
        Otvet = Otvet2;

        resultat = new jsonGETPleir();

        int elementov = 3;
        string[] jsonOtvet = new string[elementov];
        int reset = 0;
        for(int i = 0; i < Otvet.Length; i++)
        {
            switch (Otvet[i])
            {
                case '&':
                    reset++;
                    break;
                case '$':
                    reset = i+1;
                    i = Otvet.Length;
                    break;
                default:
                    jsonOtvet[reset] += Otvet[i];
                    break;
            }
        }
        resultat.Money = (float)Convert.ToDouble(jsonOtvet[0]);
        resultat.Record = (float)Convert.ToDouble(jsonOtvet[1]);
        resultat.nonitka = Convert.ToBoolean(jsonOtvet[2]);
        //////////////////////////////////////////////////////////
        elementov = 5;
        jsonOtvet = new string[elementov];
        int reset2 = 0;
        for (int i = reset;i < Otvet.Length; i++)
        {
            switch (Otvet[i])
            {
                case '&':
                    reset2++;
                    break;
                case '$':
                    jsonGETNFT NFT = new jsonGETNFT();
                    NFT.Energia = Convert.ToInt32(jsonOtvet[0]);
                    NFT.EnergiaMax = Convert.ToInt32(jsonOtvet[1]);
                    NFT.Nick = jsonOtvet[2];
                    NFT.time = Convert.ToInt32(jsonOtvet[3]); 
                    NFT.ÑlothesTip = Convert.ToInt32(jsonOtvet[4]);

                    resultat.NFT.Add(NFT);
                    jsonOtvet = new string[elementov];
                    reset2 = 0;
                    break;
                default:
                    jsonOtvet[reset2] += Otvet[i];
                    break;
            }
        }
        UndeitMenu();
        Zagruzka = false;
        renderPleir(resultat.NFT[0].ÑlothesTip);
    }


    private IEnumerator strtObuseniaCoroutine()
    {
        yield return new WaitForSeconds(3);
        pausNOvisual();
        SetingsObject.SetActive(false);
        Obusenia.SetActive(true);
        resultat.nonitka = false;
    }

    private void recordTadlica()
    {
        if (onlain && resultat.Record > 10)
            record.transform.position = new Vector2((resultat.Record * 5) + PC.StrtPosision.x, 10f);
    }


    public void smenaNFT(bool y)
    {
        NFTVID += y ? 1 : -1;
        if (NFTVID < 0) NFTVID = resultat.NFT.Count - 1;
        else if (NFTVID >= resultat.NFT.Count) NFTVID = 0;
        UndeitMenu();
    }


    public void UndeitMenu()
    {
        string Money = resultat.Money.ToString();
        string EnergiaNFT = resultat.NFT[NFTVID].Energia.ToString();
        string EnergiaMaxNFT = resultat.NFT[NFTVID].EnergiaMax.ToString();
        string NickNFT = resultat.NFT[NFTVID].Nick.ToString();
        string time = resultat.NFT[NFTVID].time.ToString();
        renderPleir(resultat.NFT[NFTVID].ÑlothesTip);
        

        PleirMenuMoney.text = Money.ToString();

        NFTMenuNeim.text = NickNFT;
        NFTMenuEnergia.text = EnergiaNFT + "/" + EnergiaMaxNFT;

        timeObject.text = time+"min";

        if (resultat.NFT.Count == 0)
            erorit("EroorNFT", false);
    }


    private void erorit(string text, bool tos = true)
    {
        if (tos)
        {
            PleiBloc = true;
            paus(true);
            Zagruzka = false;
            TimeEroor = TimeEroorStart;
        }
        //UndeitMenu();
        for (int i = 0; i < EroorVivod.Length; i++)
            EroorVivod[i].text = text;
    }


    //ïàóçà
    public void pausNOvisual()
    {
        paus(false);
        //PPV.enabled = false;
        SetingsObject.SetActive(false);
    }


    bool isIen;
    bool nervi = true;
    public void paus(bool Stop = false)
    {
        if (Zagruzka && !isPaus && !Stop && onlain && !nervi)
            return;
        nervi = false;

        if (Stop || PleiBloc)
            isPaus = false;
        else if (isIen)
            return;

        PausObject.SetActive(isPaus);
        if (isMusic)
            AMSnap[isPaus ? 0 : 1].TransitionTo(0.5f);

        if (isPaus)
            StartCoroutine(pausOff());
        else
        {
            PPV.enabled = true;
            SetingsObject.SetActive(true);
        }
        mus.SetBool("mus", isMusic);
        isPaus = !isPaus;

        if (Obusenia.activeInHierarchy)
            Obusenia.SetActive(false);
    }


    public IEnumerator pausOff()
    {
        isIen = true;
        menu.SetBool("stop", true);
        PPV.enabled = false;
        yield return new WaitForSeconds(0.15f);
        SetingsObject.SetActive(false);
        isIen = false;
    }


    //âêë/âèêë ìóçèêó
    public void Music()
    {
        if (!isMusic)
            AMSnap[isPaus ? 1 : 0].TransitionTo(0.5f);
        else
            AMSnap[2].TransitionTo(0.5f);

        isMusic = !isMusic;
        mus.SetBool("mus", isMusic);
    }


    //ðåñåò âîçðàò
    public void reset()
    {
        if (isPaus) paus();


        EndObject.SetActive(false);
        PunktObject.SetActive(true);
        US.moneuReset();
        HitStart.Invoke();

        if (onlain)
        {
            resultat.NFT[NFTVID].Energia--;
            if (resultat.NFT[NFTVID].Energia <= 0)
                vozrat();
        }
    }


    //âîçðàò â ìåíó
    public void vozrat()
    {
        if (isPaus) paus();
        SceneManager.LoadScene(0);
    }


    //kill
    public void Kill()
    {
        //PlayerPrefs.SetInt("Life", Life);
        //PlayerPrefs.SetInt("TimeKill", time);
        //if (US != null)
        //    PlayerPrefs.SetInt("Moneu", PlayerPrefs.GetInt("Moneu") + US.GetMoneu());

        paus(true);
        if (!besmert)
            end();
    }


    //âèçàâ ìåíó END
    private void end()
    {
        if (isEnd)
            return;
        isEnd = true;
        EndObject.SetActive(true);
        PausObject.SetActive(false);
        PPV.enabled = true;
        SetingsObject.SetActive(false);
        PunktObject.SetActive(false);
        Rec.text = ((int)((pleir.position.x - PC.StrtPosision.x) / 5)).ToString();
        mon.text = US.GetMoneu().ToString();


        float distans = ((int)((pleir.position.x - PC.StrtPosision.x) / 5));
        //if (onlain && distans> resultat.Record)
        if (onlain)
            resultat.Record = distans;
        

        Debug.Log("end: distans: " + distans.ToString());
        if (onlain)
            MN.PostRequest(US.GetMoneu(), distans, NFTVID);
    }


    public void EnergiaPlus()
    {
        resultat.NFT[NFTVID].Energia++;
        MN.PostRequest(US.GetMoneu(), ((int)((pleir.position.x - PC.StrtPosision.x) / 5)), NFTVID,1);
    }


    public int GetMoneuShare()
    {
        return US.GetMoneu();
    }

    public int GetDistansShare()
    {
        return ((int)((pleir.position.x - PC.StrtPosision.x) / 5));
    }

    public bool GetisPaus()
    {
        return isPaus;
    }

    public void zacrit(GameObject obgectss)
    {
        GameObject obgectsss = obgectss.transform.Find("Image").gameObject;
        Animator an = obgectsss.GetComponent<Animator>();
        if (an != null)
            an.SetBool("stop", true);
        StartCoroutine(zacritCur(obgectss));
    }

    public IEnumerator zacritCur(GameObject obgectss)
    {
        yield return new WaitForSeconds(0.15f);
        obgectss.SetActive(false);
    }

}



[System.Serializable]
class jsonGETPleir
{
    public float Money;
    public float Record;
    public bool nonitka;
    public List<jsonGETNFT> NFT =new List<jsonGETNFT>();
}

[System.Serializable]
class jsonGETNFT
{
    public int Energia;
    public int EnergiaMax;
    public string Nick;

    public int time; 
    public int ÑlothesTip;
}
