using UnityEngine;
using UnityEngine.Events;

// запуск
public class UiStart : MonoBehaviour
{
    public GameObject menu;
    public UnityEvent HitStart;
    public bool autaStart;
    public MasterLevel ML;
    public GameObject canvosOsnovnoi;
    public AudioSource audioVreibrasia;
    public Animator energia;

    private void Start()
    {
        menu.SetActive(true);
        if (ML == null)
            ML = GetComponent<MasterLevel>();
        if (autaStart) StartButton();
    }

    public void StartButton()
    {
        if (ML.onlain && ML.GetNftEnerzi())
        {
            audioVreibrasia.Play();
            energia.SetTrigger("start");
        }
        else if (!ML.onlain || !ML.GetZagruzka())
        {
            HitStart.Invoke();
            menu.SetActive(false);
            canvosOsnovnoi.SetActive(true);
        }
    }

}
