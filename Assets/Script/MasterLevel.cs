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
using Unity.Jobs;
using Unity.Collections;

public class MasterLevel : MonoBehaviour
{
    public static MasterLevel singleton { get; private set; }


    [Header("paus")]
    public GameObject SetingsObject;
    public GameObject PausObject;
    public Animator pausMenu;
    public static bool isPaus = false;//MasterLevel.isPaus
    public PostProcessVolume PPV;

    [Header("AudioMixer")]
    public Animator mus;
    public Animator mus2;
    public Animator mus3;
    public AudioMixer am;
    public AudioMixerSnapshot[] AMSnap = new AudioMixerSnapshot[2];
    public bool isMusic = false;//MasterLevel.isMusic

    [Header("Life")]
    public TextMeshProUGUI distansia;
    public int EndDistansia;
    public UiSpisac US;
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

    public TextMeshProUGUI[] EroorVivod;
    private bool Zagruzka = true;
    private bool PleiBloc = false;

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
    public int NFTVID = 0;
    private jsonGETPleir resultat;

    private bool isEnd = false;
    [Header("kill")]
    public float timeSmertStart;
    private float timeSmert;
    
    [Header("MarcetPleis")]
    public GameObject MarcetPleisObzect;
    public GameObject HomeMenuObzect;
    public TextMeshProUGUI MarcetPleisSena;
    public TextMeshProUGUI moneu;

    public AudioSource audioVreibrasia;
    public Animator energia;
    public GameObject zoznai;

    [Header("BOOL")]
    public bool onlain;
    public bool besmert;
    public bool testPlei;



    private void Awake()
    {
#if !UNITY_EDITOR
        onlain = true;
        besmert = false;
        Debug.Log("Start Bild");
#endif
        singleton = this;
        if (onlain)
            MN.GetRequest();
    }


    private void Start()
    {
        // обновления звука
        Music();

        // куширования
        if (PC == null)
            PC = pleir.GetComponent<PleirControlir>();
        if (US == null)
            US = GetComponent<UiSpisac>();

        // остановка но паузу (для главного мену)
        pausNOvisual();
        if (!onlain)
            renderPleir(1);
    }


    private void FixedUpdate()
    {
        // загрузка
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


        if(!isPaus)
            UpdeitDistansiaText();

        // умеенения времини жижни
        if (isEnd) timeSmert -= Time.fixedDeltaTime;
    }


    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.E))
            reset();
        if (Input.GetKeyDown(KeyCode.W))
            start2();
#endif
        // вклучения debug
        if (Input.GetKeyDown(KeyCode.Q))
            testFPS.SetActive(!testFPS.activeInHierarchy);

    }


    // обновления отобразения дистансий
    private void UpdeitDistansiaText()
    {
        int i = ((int)((pleir.position.x - PC.StrtPosision.x) / 5));
        if (i != EndDistansia || EndDistansia==0)
        {
            EndDistansia = i;
            distansia.text = i.ToString();
        }
    }


    // обновляет картинки у персоназев
    private void renderPleir(int znas)
    {
        pleirRender.sprite = PleirSprite[znas-1];
        pleirRender2.sprite= PleirSprite[znas-1];
    }


    // проверка возмознасти загрузатса с главного мену
    public bool GetZagruzka()
    {
        return (Zagruzka || PleiBloc) || (resultat.NFT[NFTVID].Energia <= 0);
    }


    // возрачает состоянйя енергий вибраного nft 
    public bool GetNftEnerzi()
    {
        UndeitMenu();
        print(resultat.NFT[NFTVID].Energia);
        print(NFTVID);
        return resultat.NFT[NFTVID].Energia==0;
    }


    // второи старт (бистрий рестарт)
    public void start()
    {
        singleton = this;
        
        recordTadlica();
        
        if (onlain&&resultat.nonitka)
            StartCoroutine(strtObuseniaCoroutine());
        isEnd = false;

        //востановления музики
        resetMusic();
    }


    // презагрузает информасию про ползавателя и nft
    public void resetNFT()
    {
        if (onlain)
        {
            MN.GetRequest();
            Zagruzka = true;
        }
    }


    // обновляет отобразения переклучателей на канвасе
    public void resetMusic()
    {
        mus.SetBool("mus", isMusic);
        mus2.SetBool("mus", isMusic);
        mus3.SetBool("mus", isMusic);
    }


    //(в разработке) запуск игри в тестовам резиме
    private void TestPleir()
    {
        onlain = false;
        renderPleir(1);
        
        StartCoroutine(UiStarts());
        //paus(false,true);
    }


    //(в разработке) подготовка ui для тестового резима
    private IEnumerator UiStarts() {
        yield return new WaitForSeconds(1);
        UiStart us = GetComponent<UiStart>();
        us.StartButton();
        yield return new WaitForSeconds(1);
        yield return strtObuseniaCoroutine();
        paus(false);
    } 


    // подклучения к сети
    private void conect()
    {
        string Otvet = MN.GetRequest();
        if (Otvet == "") return;
        Debug.Log("OtvetServer: " + Otvet);

        // проверка на ошибки
        switch (Otvet)
        {
            case "TestPleir":
                if (testPlei)
                {
                    TestPleir();
                    Zagruzka = false;
                    erorit("Frre");
                    PleiBloc = false;
                }
                else
                    erorit("EroorNet");
                return;
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

        //// обработк даних
        //подгатовка даних
        string Otvet2 = "";
        for (int i = 0; i < Otvet.Length; i++)
            if (Otvet[i] != '.')
                Otvet2 += Otvet[i];
            else
                Otvet2 += ',';
        Otvet = Otvet2;

        resultat = new jsonGETPleir();


        // обработка даних ползателя
        int elementov = 4;
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
        resultat.moneuCeis = Convert.ToInt32(jsonOtvet[3]);


        //обработка даних NFT
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
                    NFT.pk = Convert.ToInt32(jsonOtvet[2]);
                    NFT.time = Convert.ToInt32(jsonOtvet[3]); 
                    NFT.СlothesTip = Convert.ToInt32(jsonOtvet[4]);
                    NFT.Nick = "Runner: #" + jsonOtvet[2];


                    resultat.NFT.Add(NFT);
                    jsonOtvet = new string[elementov];
                    reset2 = 0;
                    break;
                default:
                    jsonOtvet[reset2] += Otvet[i];
                    break;
            }
        }

        // обновления всех функсий с параметрами
        Zagruzka = false;

        renderPleirStart();
        if (onlain) moneu.text = resultat.Money.ToString();
        UndeitMenu();
    }


    // проверка на енерги e NFT
    private void renderPleirStart()
    {
        for (int i =0;i< resultat.NFT.Count; i++)
        {
            if (resultat.NFT[i].Energia != 0)
            {
                NFTVID = i;
                break;
            }
        }
    }


    // стартовое обновления
    private IEnumerator strtObuseniaCoroutine()
    {
        yield return new WaitForSeconds(3);
        pausNOvisual();
        SetingsObject.SetActive(false);
        Obusenia.SetActive(true);
        if(onlain)  
            resultat.nonitka = false;
    }


    // обновлает позисию рекорда
    private void recordTadlica()
    {
        if (onlain && resultat.Record > 10)
            record.transform.position = new Vector2((resultat.Record * 5) + PC.StrtPosision.x, 10f);
    }


    // переклучения NFT
    public void smenaNFT(bool y)
    {
        if (!onlain) return;
        NFTVID += y ? 1 : -1;
        if (NFTVID < 0) NFTVID = resultat.NFT.Count - 1;
        else if (NFTVID >= resultat.NFT.Count) NFTVID = 0;
        UndeitMenu();
    }


    // подставка всех даних
    public void UndeitMenu()
    {
        // обработка
        string Money = resultat.Money.ToString();
        string EnergiaNFT = resultat.NFT[NFTVID].Energia.ToString();
        string EnergiaMaxNFT = resultat.NFT[NFTVID].EnergiaMax.ToString();
        string NickNFT = resultat.NFT[NFTVID].Nick.ToString();
        string time = resultat.NFT[NFTVID].time.ToString();
        renderPleir(resultat.NFT[NFTVID].СlothesTip);
        
        // обновления значений
        PleirMenuMoney.text = Money.ToString();

        NFTMenuNeim.text = NickNFT;
        NFTMenuEnergia.text = EnergiaNFT + "/" + EnergiaMaxNFT;

        if(time != "0")
            timeObject.text = time + "min";
        else
            timeObject.text = "full";

        MarcetPleisSena.text = resultat.moneuCeis.ToString();

        if (resultat.NFT.Count == 0)
            erorit("EroorNFT", false);
    }


    //вивод ошибки на канвос
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


    // гарантируемая пауза с не отобразениям на екране
    public void pausNOvisual()
    {
        paus(false);
        //PPV.enabled = false;
        SetingsObject.SetActive(false);
    }


    bool isIen;
    bool nervi = true;
    //пауза
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
        resetMusic();
        isPaus = !isPaus;

        if (Obusenia.activeInHierarchy)
            Obusenia.SetActive(false);
    }


    //отклучения паузи
    public IEnumerator pausOff()
    {
        isIen = true;
        pausMenu.SetBool("stop", true);
        PPV.enabled = false;
        yield return new WaitForSeconds(0.15f);
        SetingsObject.SetActive(false);
        isIen = false;
    }


    //вкл/викл музику
    public void Music()
    {
        if (!isMusic)
            AMSnap[isPaus ? 1 : 0].TransitionTo(0.5f);
        else
            AMSnap[2].TransitionTo(0.5f);

            isMusic = !isMusic;

        resetMusic();
    }


    //ресет возрат
    public void reset()
    {
        if (timeSmert > 0) return;

        if (isPaus) paus();

        PC.reset();

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

        start();
    }


    //возрат в мену
    public void vozrat()
    {
        if (isPaus) paus();
        SceneManager.LoadScene(0);
    }


    //kill
    public void Kill()
    {
        paus(true);
        if (!besmert)
            end();
    }


    //визав мену END
    private void end()
    {
        if (isEnd)
            return;
        isEnd = true;
        timeSmert = timeSmertStart;
        EndObject.SetActive(true);
        PausObject.SetActive(false);
        PPV.enabled = true;
        SetingsObject.SetActive(false);
        PunktObject.SetActive(false);
        Rec.text = ((int)((pleir.position.x - PC.StrtPosision.x) / 5)).ToString();
        mon.text = US.GetMoneu().ToString();


        float distans = ((int)((pleir.position.x - PC.StrtPosision.x) / 5));
        if (onlain && distans> resultat.Record)
            resultat.Record = distans;
        

        Debug.Log("end: distans: " + distans.ToString());
        
        if (onlain)
            MN.PostRequest(US.GetMoneu(), distans, resultat.NFT[NFTVID].pk);
    }


    // добавления енергий NFT
    public void EnergiaPlus()
    {
        if (onlain)
        {
            resultat.NFT[NFTVID].Energia++;
            MN.PostRequest(US.GetMoneu(), ((int)((pleir.position.x - PC.StrtPosision.x) / 5)), resultat.NFT[NFTVID].pk, 1);
        }
    }


    // дубликат US.GetMoneu();
    public int GetMoneuShare()
    {
        return US.GetMoneu();
    }


    // одает дистансию
    public int GetDistansShare()
    {
        return ((int)((pleir.position.x - PC.StrtPosision.x) / 5));
    }


    // get idPaus
    public bool GetisPaus()
    {
        return isPaus;
    }


    // закритее мену X
    public void zacrit(GameObject obgectss)
    {
        GameObject obgectsss = obgectss.transform.Find("Image").gameObject;
        Animator an = obgectsss.GetComponent<Animator>();
        if (an != null)
            an.SetBool("stop", true);
        StartCoroutine(zacritCur(obgectss));
    }


    // закритее-отклучения мену X 
    public IEnumerator zacritCur(GameObject obgectss)
    {
        yield return new WaitForSeconds(0.15f);
        obgectss.SetActive(false);
    }


    // MarcetPleis проверка на достаток монет и иврибрасия при не хватке (создания транзаксий)
    public void vribrasiaDolarMarcetPleis()
    {
        if (!onlain) return;
        
        if (resultat.Money >= resultat.moneuCeis)
        {
            isMusic2 = isMusic;
            if (isMusic)
                Music();
            zoznai.SetActive(true);
            StartCoroutine(barerc());
        }
        else
        {
            audioVreibrasia.Play();
            energia.SetTrigger("start");
        }
    }


    // сканирования транзаксий на конес
    public IEnumerator barerc()
    {
        bool i = ShareScreenScript.buiNft();
        //yield return new WaitForSeconds(3f);
        while (!i)
        {
            yield return new WaitForSeconds(0.1f);
            i = ShareScreenScript.buiNft();
            //Debug.Log(i + "  dasdad2222222");
            //Debug.Log("dasd");        
        }
        start2();
    }


    bool isMusic2;
    // востановления всех параметров после транзаксий
    public void start2()
    {
        if (isMusic2)
            Music();
        resetNFT();
        zoznai.SetActive(false);
        MarcetPleisObzect.SetActive(false);
        HomeMenuObzect.SetActive(true);
        Debug.Log("fin");
        //vozrat();
    }

}

// клас с параметрами про игрока
[System.Serializable]
class jsonGETPleir
{
    public float Money;
    public float Record;
    public bool nonitka;
    public int moneuCeis;
    public List<jsonGETNFT> NFT =new List<jsonGETNFT>();
}

// клас с параметрами для NFT
[System.Serializable]
class jsonGETNFT
{
    public int Energia;
    public int EnergiaMax;
    public string Nick;
    public int time;
    public int СlothesTip; 
    public int pk;
}
