using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;


// запуск
public class UiStart : MonoBehaviour
{
    public GameObject menu;
    public UnityEvent HitStart;
    public bool autaStart;
    private bool onlain;
    private bool Zagruzka;
    public MasterLevel ML;

    private void Start()
    {
        menu.SetActive(true);
        if (ML == null)
            ML = GetComponent<MasterLevel>();
        onlain = ML.onlain;
        Zagruzka = ML.Zagruzka;
        if (autaStart) StartButton();
    }

    public void StartButton()
    {
        onlain = ML.onlain;
        Zagruzka = ML.Zagruzka;

        if (!onlain || !Zagruzka)
        {
            HitStart.Invoke();
            menu.SetActive(false);
        }
    }

}
