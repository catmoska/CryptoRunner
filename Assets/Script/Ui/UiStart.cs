using UnityEngine;
using UnityEngine.Events;


// ������
public class UiStart : MonoBehaviour
{
    public GameObject menu;
    public UnityEvent HitStart;
    public bool autaStart;
    private bool onlain;
    private bool Zagruzka;
    public MasterLevel ML;
    public GameObject canvosOsnovnoi;

    private void Start()
    {
        menu.SetActive(true);
        if (ML == null)
            ML = GetComponent<MasterLevel>();
        onlain = ML.onlain;
        Zagruzka = ML.GetZagruzka();
        if (autaStart) StartButton();
    }

    public void StartButton()
    {
        onlain = ML.onlain;
        Zagruzka = ML.GetZagruzka();

        if (!onlain || !Zagruzka)
        {
            HitStart.Invoke();
            menu.SetActive(false);
            canvosOsnovnoi.SetActive(true);
        }
    }

}
