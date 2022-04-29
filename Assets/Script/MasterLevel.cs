using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class MasterLevel : MonoBehaviour
{
    [Header("paus")]
    public GameObject SetingsObject;
    public GameObject PausObject;
    public Animator menu;
    public static bool isPaus = false;//MasterLevel.isPaus
    public PostProcessVolume PPV;

    [Header("AudioMixer")]
    public Animator mus;
    public AudioMixer am;
    public AudioMixerSnapshot[] AMSnap =new AudioMixerSnapshot[2];
    public static bool isMusic = true;//MasterLevel.isMusic

    [Header("Life")]
    public Text distansia;
    public UiSpisac US;
    public bool besmert;
    public Transform pleir;
    private int Life;
    private int MaxLife = 200000;
    private int time;
    private int TimeKill;
    private int TimeKillPlus = 60;

    [Header("END")]
    public GameObject EndObject;
    public GameObject PunktObject;
    public Text mon;
    public Text Rec;
    public MasterNet MN;
    public UnityEvent HitStart;

    [Header("PleirSposobnasti")]
    public PleirControlir PC;
    public bool onlain;
    private bool Zagruzka = true;
    private bool PleiBloc = false;
    private int skin;
    public TextMeshProUGUI[] EroorVivod;

    public float TimeEroorStart;
    private float TimeEroor;


    public bool GetZagruzka()
    {
        return Zagruzka || PleiBloc;
    }


    private void Awake()
    {
        if(onlain)
            MN.GetRequest();
    }


    private void Start()
    {
        if (PC == null)
            PC = pleir.GetComponent<PleirControlir>();
        if (US == null)
            US = GetComponent<UiSpisac>();
        pausNOvisual();
    }


    private void FixedUpdate()
    {
        if (Life < MaxLife && TimeKill + TimeKillPlus == time)
        {
            TimeKill += TimeKillPlus;
            Life++;
        }

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

        distansia.text = ((int)((pleir.position.x - PC.StrtPosision.x)/5)).ToString();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            reset();
    }


    private void conect()
    {
        string Otvet = MN.GetRequest();
        if (Otvet != "")
            Debug.Log("OtvetServer: " + Otvet);

        switch (Otvet) {
            case "":
                return;
            case "EroorPleirEnergia":
                erorit("EroorPleirEnergia");
                return;
            case "EroorNFT":
                erorit("EroorNFT");
                return;
            case "EroorNFTEnergia":
                erorit("EroorNFTEnergia");
                return;
            case "EroorNoPlei":
                erorit("EroorNoPlei");
                return;
            case "EroorNet":
                erorit("EroorNet");
                return;
            default:
                erorit("", false);
                Zagruzka = false;
                PleiBloc = false;
                break;
        }
        

        //���������

    }


    private void erorit(string text,bool tos = true)
    {
        if (tos)
        {
            PleiBloc = true;
            paus(true);
            Zagruzka = false;
            TimeEroor = TimeEroorStart;
        }
        for (int i = 0; i< EroorVivod.Length;i++)
            EroorVivod[i].text = text;
    }

    //��������� ���������� ������
    public int Getlife()
    {
        return Life;
    }


    //�����
    public void pausNOvisual()
    {
        paus(false);
        PPV.enabled = false;
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


    //���/���� ������
    public void Music()
    {
        if (!isMusic)
            AMSnap[isPaus ? 1 : 0].TransitionTo(0.5f);
        else 
            AMSnap[2].TransitionTo(0.5f);

        isMusic = !isMusic;
        mus.SetBool("mus", isMusic);
    }

    //����� ������
    public void reset()
    {
        if (isPaus) paus();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (onlain)
            Zagruzka = true;
        

        EndObject.SetActive(false);
        PunktObject.SetActive(true);
        US.moneuReset();
        HitStart.Invoke();
    }

    //������ � ����
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

    //����� ���� END
    private void end()
    {
        EndObject.SetActive(true);
        PausObject.SetActive(false);
        PPV.enabled = true;
        SetingsObject.SetActive(false);
        PunktObject.SetActive(false);
        Rec.text = ((int)((pleir.position.x - PC.StrtPosision.x) / 5)).ToString();
        mon.text = US.GetMoneu().ToString();

        if(onlain)
            MN.PostRequest(US.GetMoneu(), ((int)((pleir.position.x - PC.StrtPosision.x) / 5)));
    }

}
