using UnityEngine;
using TMPro;

//читает количество монет и виводит
public class UiSpisac : MonoBehaviour
{
    public MasterLevel ML;
    public GameObject menu;
    public TextMeshProUGUI moneuText;
    private int Moneu;

    private void Start()
    {
        if (ML == null)
            ML = GetComponent<MasterLevel>();
        moneuText.text = "0";
    }

    public void MoneuPlus()
    {
        //Debug.Log("MoneuPlus");
        Moneu++;
        moneuText.text = Moneu.ToString();
    }

    public int GetMoneu()
    {
        return Moneu;
    }

    public void moneuReset()
    {
        Moneu = 0;
        moneuText.text = "0";
    }
}
